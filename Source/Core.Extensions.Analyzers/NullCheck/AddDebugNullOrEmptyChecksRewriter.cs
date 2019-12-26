using System;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

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
                        new[] { SyntaxFactory.IdentifierName(parameterName) })),
                _ => generator.LogicalNotExpression(
                    SyntaxFactory.IsPatternExpression(
                    SyntaxFactory.IdentifierName(parameterName),
                    SyntaxFactory.ConstantPattern(
                        SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression))))
            };
            var nullCheckStatement = generator.ExpressionStatement(
                generator.InvocationExpression(
                    generator.MemberAccessExpression(
                        generator.IdentifierName(nameof(Debug)),
                        generator.IdentifierName(nameof(Debug.Assert))),
                    new[] { assertExpression }))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine));
            return (ExpressionStatementSyntax)nullCheckStatement;
        }
    }
}
