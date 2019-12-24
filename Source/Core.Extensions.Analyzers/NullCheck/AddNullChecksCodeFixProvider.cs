using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions.Analyzers.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.NullCheck
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public class AddNullChecksCodeFixProvider : CodeFixProvider
    {
        public static string Title { get; } = Strings.AddNullChecksTitle;

        public override ImmutableArray<string> FixableDiagnosticIds { get; }
            = ImmutableArray.Create(NullCheckAnalyzer.Id);

        public override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
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

            var addNullChecksRewriter = new AddNullChecksRewriter(document, model, nullableParameters, token);
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
                var methodNullCheckDiagnostics = NullCheckAnalyzer.GetDiagnostics(node, model, token);
                if (methodNullCheckDiagnostics.Length > 1)
                {
                    var codeAction = CodeAction.Create(
                      Title,
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
                      equivalenceKey: Title);
                    context.RegisterCodeFix(codeAction, diagnostic);
                }
            }
        }
    }
}
