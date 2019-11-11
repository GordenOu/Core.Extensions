using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.Extensions.Analyzers.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Core.Extensions.Analyzers.NullCheck
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public class AddNullCheckCodeFixProvider : CodeFixProvider
    {
        private static readonly string title = Strings.AddNullCheckTitle;

        public override ImmutableArray<string> FixableDiagnosticIds { get; }
            = ImmutableArray.Create(NullCheckAnalyzer.Id);

        public override FixAllProvider GetFixAllProvider()
        {
            return WellKnownFixAllProviders.BatchFixer;
        }

        private Task<Document> FixDiagnostic(
            Document document,
            SyntaxNode root,
            SemanticModel model,
            Diagnostic diagnostic,
            CancellationToken token)
        {
            if (diagnostic.Id != NullCheckAnalyzer.Id || diagnostic.AdditionalLocations.Count != 1)
            {
                return Task.FromResult(document);
            }

            var node = root.FindNode(diagnostic.AdditionalLocations[0].SourceSpan);

            var nullableParametersVisitor = new NullableParametersVisitor(model, token);
            nullableParametersVisitor.Visit(node);
            var nullableParameters = nullableParametersVisitor.NullableParameters;

            if (!diagnostic.Properties.TryGetValue(nameof(NullableParameter.Index), out string index)
                || !int.TryParse(index, out int parameterIndex))
            {
                return Task.FromResult(document);
            }
            var nullableParameter = nullableParameters.SingleOrDefault(x => x.Index == parameterIndex);
            if (nullableParameter is null)
            {
                return Task.FromResult(document);
            }

            var addNullCheckRewriter = new AddNullCheckRewriter(document, model, nullableParameter, token);
            var newNode = addNullCheckRewriter.Visit(node);
            var newDocument = document.WithSyntaxRoot(root.ReplaceNode(node, newNode));
            return Task.FromResult(newDocument);
        }

        public override async Task RegisterCodeFixesAsync(CodeFixContext context)
        {
            var document = context.Document;
            var syntaxRoot = await document.GetSyntaxRootAsync(context.CancellationToken);
            var semanticModel = await document.GetSemanticModelAsync(context.CancellationToken);
            foreach (var diagnostic in context.Diagnostics)
            {
                var codeAction = CodeAction.Create(
                    title,
                    token => FixDiagnostic(document, syntaxRoot, semanticModel, diagnostic, token),
                    equivalenceKey: title);
                context.RegisterCodeFix(codeAction, diagnostic);
            }
        }
    }
}
