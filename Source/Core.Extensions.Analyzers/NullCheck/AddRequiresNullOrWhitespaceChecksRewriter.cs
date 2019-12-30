using System;
using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class AddRequiresNullOrWhitespaceChecksRewriter : AddNullChecksRewriter
    {
        public AddRequiresNullOrWhitespaceChecksRewriter(
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
            string nullCheckMethod = "NotNull";
            if (nullableParameter.Symbol.Type.Kind == SymbolKind.PointerType)
            {
                nullCheckMethod = "NotNullPtr";
            }
            else if (nullableParameter.Symbol.Type.SpecialType == SpecialType.System_String)
            {
                nullCheckMethod = "NotNullOrWhitespace";
            }
            var generator = SyntaxGenerator.GetGenerator(document);
            var nullCheckStatement = generator.ExpressionStatement(
                generator.InvocationExpression(
                    generator.MemberAccessExpression(
                        generator.IdentifierName("Requires"),
                        generator.IdentifierName(nullCheckMethod)),
                    new[]
                    {
                        generator.IdentifierName(parameterName),
                        generator.NameOfExpression(generator.IdentifierName(parameterName))
                    }))
                .WithTrailingTrivia(SyntaxFactory.EndOfLine(Environment.NewLine));
            return (ExpressionStatementSyntax)nullCheckStatement;
        }
    }
}
