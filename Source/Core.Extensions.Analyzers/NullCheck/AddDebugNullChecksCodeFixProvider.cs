using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Core.Extensions.Analyzers.NullCheck
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public class AddDebugNullChecksCodeFixProvider : AddNullChecksCodeFixProvider
    {
        public override string AddNullCheckTitle { get; } = Strings.AddDebugNullCheckTitle;

        public override string AddNullChecksTitle { get; } = Strings.AddDebugNullChecksTitle;

        public override AddNullChecksRewriter GetRewriter(
            Document document,
            SemanticModel model,
            ImmutableArray<NullableParameter> nullableParameters,
            CancellationToken token)
        {
            return new AddDebugNullChecksRewriter(document, model, nullableParameters, token);
        }
    }
}
