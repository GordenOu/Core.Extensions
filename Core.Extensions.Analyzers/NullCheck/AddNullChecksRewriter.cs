using System.Collections.Immutable;
using Core.Extensions.Analyzers.SyntaxExtensions;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.NullCheck;

public abstract class AddNullChecksRewriter : CSharpSyntaxRewriter
{
    private readonly Document document;
    private readonly SemanticModel model;
    private readonly ImmutableArray<NullableParameter> nullableParameters;
    private readonly CancellationToken token;

    private ImmutableArray<ExistingNullCheck> existingNullChecks;

    public AddNullChecksRewriter(
        Document document,
        SemanticModel model,
        ImmutableArray<NullableParameter> nullableParameters,
        CancellationToken token)
    {
        this.document = document;
        this.model = model;
        this.nullableParameters = nullableParameters;
        this.token = token;
        existingNullChecks = ImmutableArray<ExistingNullCheck>.Empty;
    }

    private void VisitBaseMethodDeclaration(CSharpSyntaxNode node)
    {
        var existingNullChecksVisitor = new ExistingNullChecksVisitor(model, token);
        existingNullChecksVisitor.Visit(node);
        existingNullChecks = existingNullChecksVisitor.ExistingNullChecks;
    }

    public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
    {
        VisitBaseMethodDeclaration(node);
        return base.VisitConstructorDeclaration(node);
    }

    public override SyntaxNode? VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
    {
        VisitBaseMethodDeclaration(node);
        return base.VisitConversionOperatorDeclaration(node);
    }

    public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node)
    {
        VisitBaseMethodDeclaration(node);
        return base.VisitMethodDeclaration(node);
    }

    public override SyntaxNode? VisitOperatorDeclaration(OperatorDeclarationSyntax node)
    {
        VisitBaseMethodDeclaration(node);
        return base.VisitOperatorDeclaration(node);
    }

    public override SyntaxNode? VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
    {
        VisitBaseMethodDeclaration(node);
        return base.VisitLocalFunctionStatement(node);
    }

    public abstract ExpressionStatementSyntax GenerateNullCheckStatement(
        Document document,
        NullableParameter nullableParameter);

    public override SyntaxNode VisitBlock(BlockSyntax node)
    {
        var nullCheckStatements = new List<ExpressionStatementSyntax>();
        foreach (var nullableParameter in nullableParameters)
        {
            var nullCheckStatement = GenerateNullCheckStatement(document, nullableParameter);
            nullCheckStatements.Add(nullCheckStatement);
        }
        var statements = node.Statements;
        if (existingNullChecks.IsEmpty)
        {
            statements = statements.InsertRange(0, nullCheckStatements);
            if (statements.Count > nullCheckStatements.Count)
            {
                var nextStatement = statements[nullCheckStatements.Count];
                statements = statements.Replace(
                    nextStatement,
                    nextStatement.InsertLeadingEndOfLine());
            }
        }
        else if (
            existingNullChecks.Last().StatementIndex == existingNullChecks.Length - 1
            && existingNullChecks.SequenceEqual(existingNullChecks.OrderBy(x => x.ParameterIndex)))
        {
            var existingNullCheckIndexes = existingNullChecks
                .Select(nullCheck => nullCheck.ParameterIndex)
                .ToList();
            for (int i = 0; i < nullableParameters.Length; i++)
            {
                var nullableParameter = nullableParameters[i];
                var nullCheckStatement = nullCheckStatements[i];
                int insertIndex = existingNullCheckIndexes.Count(index => index < nullableParameter.Index);
                statements = statements.Insert(insertIndex, nullCheckStatement);
                existingNullCheckIndexes.Insert(insertIndex, nullableParameter.Index);
            }
            if (statements.Count > existingNullCheckIndexes.Count + 1)
            {
                var nextStatement = statements[existingNullCheckIndexes.Count + 1];
                statements = statements.Replace(
                    nextStatement,
                    nextStatement.InsertLeadingEndOfLine());
            }
        }
        else
        {
            statements = statements.InsertRange(0, nullCheckStatements);
        }
        return node.WithStatements(statements);
    }
}
