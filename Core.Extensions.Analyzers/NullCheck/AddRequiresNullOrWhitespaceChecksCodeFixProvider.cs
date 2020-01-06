using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Core.Extensions.Analyzers.NullCheck
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public class AddRequiresNullOrWhitespaceChecksCodeFixProvider : AddNullChecksCodeFixProvider
    {
        public override string AddNullCheckTitle { get; } = Strings.AddRequiresNullOrWhitespaceCheckTitle;

        public override string AddNullChecksTitle { get; } = Strings.AddRequiresNullOrWhitespaceChecksTitle;

        public override bool ShouldRegisterCodeFix(NullableParameter parameter)
        {
            return parameter.Symbol.Type.SpecialType == SpecialType.System_String;
        }

        public override AddNullChecksRewriter GetRewriter(
            Document document,
            SemanticModel model,
            ImmutableArray<NullableParameter> nullableParameters,
            CancellationToken token)
        {
            return new AddRequiresNullOrWhitespaceChecksRewriter(document, model, nullableParameters, token);
        }
    }
}
