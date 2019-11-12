using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Editing;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class AddNullCheckRewriter : CSharpSyntaxRewriter
    {
        private readonly Document document;
        private readonly SemanticModel model;
        private readonly NullableParameter nullableParameter;
        private readonly CancellationToken token;

        private ImmutableArray<ExistingNullCheck> existingNullChecks;

        public AddNullCheckRewriter(
            Document document,
            SemanticModel model,
            NullableParameter nullableParameter,
            CancellationToken token)
        {
            this.document = document;
            this.model = model;
            this.nullableParameter = nullableParameter;
            this.token = token;
        }

        public override SyntaxNode VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            var existingNullChecksVisitor = new ExistingNullChecksVisitor(model, token);
            existingNullChecksVisitor.Visit(node);
            existingNullChecks = existingNullChecksVisitor.ExistingNullChecks;
            return base.VisitMethodDeclaration(node);
        }

        public override SyntaxNode VisitBlock(BlockSyntax node)
        {
            static StatementSyntax InsertLeadingEndOfLine(StatementSyntax statement)
            {
                var leadingTrivia = statement.GetLeadingTrivia();
                if (leadingTrivia.FirstOrDefault().IsKind(SyntaxKind.EndOfLineTrivia))
                {
                    return statement;
                }
                else
                {
                    leadingTrivia = leadingTrivia.Insert(0, SyntaxFactory.EndOfLine(Environment.NewLine));
                    return statement.WithLeadingTrivia(leadingTrivia);
                }
            }

            string parameterName = nullableParameter.Syntax.Identifier.Text;
            var nullCheckMethod = "NotNull";
            if (nullableParameter.Symbol.Type.Kind == SymbolKind.PointerType)
            {
                nullCheckMethod = "NotNullPtr";
            }
            var generator = SyntaxGenerator.GetGenerator(document);
            var nullCheckStatement = (ExpressionStatementSyntax)generator.ExpressionStatement(
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
            var statements = node.Statements;
            if (existingNullChecks.IsEmpty)
            {
                statements = statements.Insert(0, nullCheckStatement);
                if (statements.Count > 1)
                {
                    var nextStatement = statements[1];
                    statements = statements.Replace(
                        nextStatement,
                        InsertLeadingEndOfLine(nextStatement));
                }
            }
            else if (
                existingNullChecks.Last().StatementIndex == existingNullChecks.Length - 1
                && existingNullChecks.SequenceEqual(existingNullChecks.OrderBy(x => x.ParameterIndex)))
            {
                int insertIndex = existingNullChecks.Count(x => x.ParameterIndex < nullableParameter.Index);
                statements = statements.Insert(insertIndex, nullCheckStatement);
                if (statements.Count > existingNullChecks.Length + 1)
                {
                    var nextStatement = statements[existingNullChecks.Length + 1];
                    statements = statements.Replace(
                        nextStatement,
                        InsertLeadingEndOfLine(nextStatement));
                }
            }
            else
            {
                statements = statements.Insert(0, nullCheckStatement);
            }
            return node.WithStatements(statements);
        }
    }
}
