using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Core.Extensions.Analyzers.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public class AddNullCheckCodeFixProvider : CodeFixProvider
    {
        private static readonly string title = Strings.AddNullCheckTitle;

        public override ImmutableArray<string> FixableDiagnosticIds { get; }
            = ImmutableArray.Create(AddNullCheckCodeAnalyzer.Id);

        public override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var diagnostic = context.Diagnostics.FirstOrDefault();
            if (diagnostic == null)
            {
                return;
            }

            var document = context.Document;
            var syntaxRoot = await document.GetSyntaxRootAsync(context.CancellationToken);
            if (!(syntaxRoot.FindNode(diagnostic.Location.SourceSpan) is ParameterSyntax parameterSyntax))
            {
                return;
            }
            if (!(parameterSyntax.Parent is ParameterListSyntax parameterListSyntax))
            {
                return;
            }
            var bodyLocation = diagnostic.AdditionalLocations.FirstOrDefault();
            if (bodyLocation is null)
            {
                return;
            }
            if (!(syntaxRoot.FindNode(bodyLocation.SourceSpan) is BlockSyntax bodySyntax))
            {
                return;
            }

            // If existing null checks are ordered, Add null check before the null check of the next parameter.
            var sementicModel = await context.Document.GetSemanticModelAsync(context.CancellationToken);
            if (!(sementicModel.GetDeclaredSymbol(parameterSyntax, context.CancellationToken)
                    is IParameterSymbol parameterSymbol))
            {
                return;
            }
            var parameterList = parameterListSyntax.Parameters
                .Select(node => sementicModel.GetDeclaredSymbol(node, context.CancellationToken))
                .OfType<IParameterSymbol>()
                .ToList();
            int parameterIndex = parameterList.FindIndex(parameter => parameter.Equals(parameterSymbol));
            if (parameterIndex == -1)
            {
                return;
            }
            var existingNullChecks = diagnostic.AdditionalLocations.Skip(1)
                .Select(location => syntaxRoot.FindNode(location.SourceSpan))
                .OfType<ExpressionStatementSyntax>()
                .ToArray();
            bool isExistingNullCheckOrdered = true;
            int nullCheckIndex = -1;
            ExpressionStatementSyntax nextNullCheckStatement = null;
            foreach (var node in existingNullChecks)
            {
                var symbol = sementicModel.GetOperation(node, context.CancellationToken);
                var expressionStatement = symbol as IExpressionStatementOperation;
                var invocationOperation = expressionStatement?.Operation as IInvocationOperation;
                var reference = invocationOperation?.Arguments.FirstOrDefault()?.Value as IParameterReferenceOperation;
                var nullCheckParameter = reference?.Parameter;
                if (nullCheckParameter is null)
                {
                    return;
                }
                else
                {
                    int index = parameterList.FindIndex(parameter => parameter.Equals(nullCheckParameter));
                    if (index < nullCheckIndex)
                    {
                        isExistingNullCheckOrdered = false;
                        break;
                    }
                    else
                    {
                        nullCheckIndex = index;
                        if (nextNullCheckStatement is null && index > parameterIndex)
                        {
                            nextNullCheckStatement = (ExpressionStatementSyntax)expressionStatement.Syntax;
                        }
                    }
                }
            }
            int insertIndex = 0;
            if (isExistingNullCheckOrdered)
            {
                if (nextNullCheckStatement is null)
                {
                    var lastNullCheckStatement = existingNullChecks.LastOrDefault();
                    if (lastNullCheckStatement != null)
                    {
                        insertIndex = bodySyntax.Statements
                           .Select((statement, index) => (statement, index))
                           .FirstOrDefault(_ => _.statement.Span.Equals(lastNullCheckStatement.Span))
                           .index + 1;
                    }
                }
                else
                {
                    insertIndex = bodySyntax.Statements
                       .Select((statement, index) => (statement, index))
                       .FirstOrDefault(_ => _.statement.Span.Equals(nextNullCheckStatement.Span))
                       .index;
                }
            }

            var codeAction = CodeAction.Create(
                title,
                token =>
                {
                    string parameterName = parameterSyntax.Identifier.Text;
                    var generator = SyntaxGenerator.GetGenerator(context.Document);
                    var nullCheckStatement =
                        (ExpressionStatementSyntax)generator.ExpressionStatement(
                            generator.InvocationExpression(
                                generator.MemberAccessExpression(
                                    generator.IdentifierName("Requires"),
                                    generator.IdentifierName("NotNull")),
                                new[]
                                {
                                    generator.IdentifierName(parameterName),
                                    generator.NameOfExpression(generator.IdentifierName(parameterName))
                                }));
                    var statements = bodySyntax.Statements.ToList();
                    statements.Insert(insertIndex, nullCheckStatement);
                    var newBody = bodySyntax
                        .WithStatements(new SyntaxList<StatementSyntax>(statements))
                        .WithTriviaFrom(bodySyntax);
                    var newRoot = syntaxRoot.ReplaceNode(bodySyntax, newBody);
                    var newDocument = context.Document.WithSyntaxRoot(newRoot);
                    return Task.FromResult(newDocument);
                },
                equivalenceKey: title);
            context.RegisterCodeFix(codeAction, diagnostic);
        }
    }
}
