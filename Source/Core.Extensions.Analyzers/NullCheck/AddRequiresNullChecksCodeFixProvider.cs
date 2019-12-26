using System.Collections.Immutable;
using System.Threading;
using Core.Extensions.Analyzers.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Core.Extensions.Analyzers.NullCheck
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public class AddRequiresNullChecksCodeFixProvider : AddNullChecksCodeFixProvider
    {
        public override string Title { get; } = Strings.AddRequiresNullChecksTitle;

        public override AddNullChecksRewriter GetRewriter(
            Document document,
            SemanticModel model,
            ImmutableArray<NullableParameter> nullableParameters,
            CancellationToken token)
        {
            return new AddRequiresNullChecksRewriter(document, model, nullableParameters, token);
        }
    }
}
