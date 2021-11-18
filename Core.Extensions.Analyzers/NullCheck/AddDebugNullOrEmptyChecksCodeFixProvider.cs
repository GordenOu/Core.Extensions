using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Core.Extensions.Analyzers.NullCheck;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public class AddDebugNullOrEmptyChecksCodeFixProvider : AddNullChecksCodeFixProvider
{
    public override string AddNullCheckTitle { get; } = Strings.AddDebugNullOrEmptyCheckTitle;

    public override string AddNullChecksTitle { get; } = Strings.AddDebugNullOrEmptyChecksTitle;

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
        return new AddDebugNullOrEmptyChecksRewriter(document, model, nullableParameters, token);
    }
}
