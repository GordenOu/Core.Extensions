using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.NullCheck
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public abstract class AddNullChecksCodeFixProvider : CodeFixProvider
    {
        public abstract string AddNullCheckTitle { get; }

        public abstract string AddNullChecksTitle { get; }

        public override ImmutableArray<string> FixableDiagnosticIds { get; }
            = ImmutableArray.Create(NullCheckAnalyzer.Id);

        public override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        public virtual bool FilterDiagnostic(NullableParameter parameter) => true;

        public abstract AddNullChecksRewriter GetRewriter(
            Document document,
            SemanticModel model,
            ImmutableArray<NullableParameter> nullableParameters,
            CancellationToken token);

        private Document FixDiagnostic(
            Document document,
            NullableParameter nullableParameter,
            SyntaxNode root,
            SemanticModel model,
            SyntaxNode node,
            CancellationToken token)
        {
            var addNullChecksRewriter = GetRewriter(
                document,
                model,
                ImmutableArray.Create(nullableParameter),
                token);
            var newNode = addNullChecksRewriter.Visit(node);
            var newDocument = document.WithSyntaxRoot(root.ReplaceNode(node, newNode));
            return newDocument;
        }

        private Document FixDiagnostics(
            Document document,
            ImmutableArray<Diagnostic> diagnostics,
            SyntaxNode root,
            SemanticModel model,
            SyntaxNode node,
            CancellationToken token)
        {
            var parameterIndexes = new List<int>();
            foreach (var diagnostic in diagnostics)
            {
                if (diagnostic.Properties.TryGetValue(nameof(NullableParameter.Index), out string index)
                    && int.TryParse(index, out int parameterIndex))
                {
                    parameterIndexes.Add(parameterIndex);
                }
            }

            var nullableParametersVisitor = new NullableParametersVisitor(model, token);
            nullableParametersVisitor.Visit(node);
            var nullableParameters = nullableParametersVisitor.NullableParameters
                .Where(x => parameterIndexes.Contains(x.Index))
                .ToImmutableArray();

            var addNullChecksRewriter = GetRewriter(document, model, nullableParameters, token);
            var newNode = addNullChecksRewriter.Visit(node);
            var newDocument = document.WithSyntaxRoot(root.ReplaceNode(node, newNode));
            return newDocument;
        }

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var document = context.Document;
            var token = context.CancellationToken;
            var root = await document.GetSyntaxRootAsync(token);
            if (root is null)
            {
                return;
            }
            var model = await document.GetSemanticModelAsync(token);
            if (model is null)
            {
                return;
            }

            foreach (var diagnostic in context.Diagnostics)
            {
                if (diagnostic.Id != NullCheckAnalyzer.Id)
                {
                    continue;
                }
                var node = root.FindNode(diagnostic.Location.SourceSpan)
                    .Ancestors()
                    .OfType<MethodDeclarationSyntax>()
                    .FirstOrDefault();
                if (node is null)
                {
                    continue;
                }
                if (!diagnostic.Properties.TryGetValue(nameof(NullableParameter.Index), out string index)
                || !int.TryParse(index, out int parameterIndex))
                {
                    continue;
                }
                var nullableParametersVisitor = new NullableParametersVisitor(model, token);
                nullableParametersVisitor.Visit(node);
                var nullableParameters = nullableParametersVisitor.NullableParameters;
                var nullableParameter = nullableParameters.SingleOrDefault(x => x.Index == parameterIndex);
                if (nullableParameter is null)
                {
                    continue;
                }
                if (!FilterDiagnostic(nullableParameter))
                {
                    continue;
                }

                // Add null check.
                {
                    var codeAction = CodeAction.Create(
                    AddNullCheckTitle,
                    token =>
                    {
                        var newDocument = FixDiagnostic(document, nullableParameter, root, model, node, token);
                        return Task.FromResult(newDocument);
                    },
                    equivalenceKey: AddNullCheckTitle);
                    context.RegisterCodeFix(codeAction, diagnostic);
                }

                // Add null checks.
                var methodNullCheckDiagnostics = NullCheckAnalyzer.GetDiagnostics(node, model, token);
                if (methodNullCheckDiagnostics.Length > 1)
                {
                    var codeAction = CodeAction.Create(
                      AddNullChecksTitle,
                      token =>
                      {
                          var newDocument = FixDiagnostics(
                              document,
                              methodNullCheckDiagnostics,
                              root,
                              model,
                              node,
                              token);
                          return Task.FromResult(newDocument);
                      },
                      equivalenceKey: AddNullChecksTitle);
                    context.RegisterCodeFix(codeAction, diagnostic);
                }
            }
        }
    }
}
