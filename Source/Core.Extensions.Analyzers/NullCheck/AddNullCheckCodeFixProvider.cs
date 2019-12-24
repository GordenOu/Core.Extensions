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
    public class AddNullCheckCodeFixProvider : CodeFixProvider
    {
        public static string Title { get; } = Strings.AddNullCheckTitle;

        public override ImmutableArray<string> FixableDiagnosticIds { get; }
            = ImmutableArray.Create(NullCheckAnalyzer.Id);

        public override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        private Document FixDiagnostic(
            Document document,
            Diagnostic diagnostic,
            SyntaxNode root,
            SemanticModel model,
            SyntaxNode node,
            CancellationToken token)
        {
            if (!diagnostic.Properties.TryGetValue(nameof(NullableParameter.Index), out string index)
                || !int.TryParse(index, out int parameterIndex))
            {
                return document;
            }

            var nullableParametersVisitor = new NullableParametersVisitor(model, token);
            nullableParametersVisitor.Visit(node);
            var nullableParameters = nullableParametersVisitor.NullableParameters;
            var nullableParameter = nullableParameters.SingleOrDefault(x => x.Index == parameterIndex);
            if (nullableParameter is null)
            {
                return document;
            }

            var addNullChecksRewriter = new AddNullChecksRewriter(
                document,
                model,
                ImmutableArray.Create(nullableParameter),
                token);
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
                var codeAction = CodeAction.Create(
                    Title,
                    token =>
                    {
                        var newDocument = FixDiagnostic(document, diagnostic, root, model, node, token);
                        return Task.FromResult(newDocument);
                    },
                    equivalenceKey: Title);
                context.RegisterCodeFix(codeAction, diagnostic);
            }
        }
    }
}
