using System.Collections.Immutable;
using System.Threading;
using Core.Extensions.Analyzers.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Core.Extensions.Analyzers.NullCheck
{
    [ExportCodeFixProvider(LanguageNames.CSharp)]
    public class AddRequiresNullOrEmptyChecksCodeFixProvider : AddNullChecksCodeFixProvider
    {
        public override string AddNullCheckTitle { get; } = Strings.AddRequiresNullOrEmptyCheckTitle;

        public override string AddNullChecksTitle { get; } = Strings.AddRequiresNullOrEmptyChecksTitle;

        public override bool ShouldRegisterCodeFix(NullableParameter parameter)
        {
            return parameter.Symbol.Type.Kind == SymbolKind.ArrayType
                || parameter.Symbol.Type.SpecialType == SpecialType.System_String;
        }

        public override AddNullChecksRewriter GetRewriter(
            Document document,
            SemanticModel model,
            ImmutableArray<NullableParameter> nullableParameters,
            CancellationToken token)
        {
            return new AddRequiresNullOrEmptyChecksRewriter(document, model, nullableParameters, token);
        }
    }
}
