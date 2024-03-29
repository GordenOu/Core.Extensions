using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Core.Extensions.Analyzers.NullCheck;

public class AddDebugNullChecksRewriter : AddNullChecksRewriter
{
    public AddDebugNullChecksRewriter(
        Document document,
        SemanticModel model,
        ImmutableArray<NullableParameter> nullableParameters,
        CancellationToken token)
        : base(document, model, nullableParameters, token)
    { }

    public override ExpressionStatementSyntax GenerateNullCheckStatement(
        Document document,
        NullableParameter nullableParameter)
    {
        string parameterName = nullableParameter.Syntax.Identifier.Text;
        var generator = SyntaxGenerator.GetGenerator(document);
        var nullCheckStatement = generator.ExpressionStatement(
            generator.InvocationExpression(
                generator.MemberAccessExpression(
                    generator.IdentifierName(nameof(Debug)),
                    generator.IdentifierName(nameof(Debug.Assert))),
                new[]
                {
                    IsPatternExpression(
                        IdentifierName(parameterName),
                        UnaryPattern(
                            ConstantPattern(
                                LiteralExpression(SyntaxKind.NullLiteralExpression))))
                }))
            .WithTrailingTrivia(EndOfLine(Environment.NewLine));
        return (ExpressionStatementSyntax)nullCheckStatement;
    }
}
