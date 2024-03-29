using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeFixes;

namespace Core.Extensions.Analyzers.NullCheck;

[ExportCodeFixProvider(LanguageNames.CSharp)]
public class AddRequiresNullChecksCodeFixProvider : AddNullChecksCodeFixProvider
{
    public override string AddNullCheckTitle { get; } = Strings.AddRequiresNullCheckTitle;

    public override string AddNullChecksTitle { get; } = Strings.AddRequiresNullChecksTitle;

    public override AddNullChecksRewriter GetRewriter(
        Document document,
        SemanticModel model,
        ImmutableArray<NullableParameter> nullableParameters,
        CancellationToken token)
    {
        return new AddRequiresNullChecksRewriter(document, model, nullableParameters, token);
    }
}
