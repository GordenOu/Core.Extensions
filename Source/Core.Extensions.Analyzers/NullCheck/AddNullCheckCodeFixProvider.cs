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

        private async Task<Document> FixDiagnostic(
            Document document,
            Diagnostic diagnostic,
            CancellationToken token)
        {
            if (!diagnostic.Properties.TryGetValue(nameof(NullableParameter.Index), out string index)
                || !int.TryParse(index, out int parameterIndex))
            {
                return document;
            }

            var root = await document.GetSyntaxRootAsync(token);
            var node = root.FindNode(diagnostic.Location.SourceSpan)
                .Ancestors()
                .OfType<MethodDeclarationSyntax>()
                .FirstOrDefault();
            if (node is null)
            {
                return document;
            }

            var model = await document.GetSemanticModelAsync(token);

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

        public override Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            foreach (var diagnostic in context.Diagnostics)
            {
                if (diagnostic.Id != NullCheckAnalyzer.Id)
                {
                    continue;
                }
                var codeAction = CodeAction.Create(
                    Title,
                    token => FixDiagnostic(context.Document, diagnostic, token),
                    equivalenceKey: Title);
                context.RegisterCodeFix(codeAction, diagnostic);
            }
            return Task.CompletedTask;
        }
    }
}
