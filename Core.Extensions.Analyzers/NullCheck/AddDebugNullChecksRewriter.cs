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
                        generator.LogicalNotExpression(
                            SyntaxFactory.IsPatternExpression(
                                SyntaxFactory.IdentifierName(parameterName),
                                SyntaxFactory.ConstantPattern(
                                    SyntaxFactory.LiteralExpression(SyntaxKind.NullLiteralExpression))))
                    }))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine));
            return (ExpressionStatementSyntax)nullCheckStatement;
        }
    }
}
