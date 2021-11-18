using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Core.Extensions.Analyzers.NullCheck;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public class AddDebugNullOrWhitespaceChecksCodeFixProvider : AddNullChecksCodeFixProvider
{
    public override string AddNullCheckTitle { get; } = Strings.AddDebugNullOrWhitespaceCheckTitle;

    public override string AddNullChecksTitle { get; } = Strings.AddDebugNullOrWhitespaceChecksTitle;

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
        return new AddDebugNullOrWhitespaceChecksRewriter(document, model, nullableParameters, token);
    }
}
