using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;
using static Microsoft.CodeAnalysis.CSharp.SyntaxFactory;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class AddDebugNullOrEmptyChecksRewriter : AddNullChecksRewriter
    {
        public AddDebugNullOrEmptyChecksRewriter(
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
            SyntaxNode assertExpression = nullableParameter.Symbol.Type.SpecialType switch
            {
                SpecialType.System_String => generator.LogicalNotExpression(
                    generator.InvocationExpression(
                        generator.MemberAccessExpression(
                            generator.TypeExpression(SpecialType.System_String),
                            generator.IdentifierName(nameof(string.IsNullOrEmpty))),
                        new[] { IdentifierName(parameterName) })),
                _ => IsPatternExpression(
                    IdentifierName(parameterName),
                    UnaryPattern(
                        ConstantPattern(
                            LiteralExpression(SyntaxKind.NullLiteralExpression))))
            };
            var nullCheckStatement = generator.ExpressionStatement(
                generator.InvocationExpression(
                    generator.MemberAccessExpression(
                        generator.IdentifierName(nameof(Debug)),
                        generator.IdentifierName(nameof(Debug.Assert))),
                    new[] { assertExpression }))
                .WithTrailingTrivia(EndOfLine(Environment.NewLine));
            return (ExpressionStatementSyntax)nullCheckStatement;
        }
    }
}
