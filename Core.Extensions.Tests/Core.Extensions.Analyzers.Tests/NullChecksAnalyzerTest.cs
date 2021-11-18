using System;
using System.Collections.Immutable;
using System.Linq;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.Tests;

public class NullChecksAnalyzerTest : AnalyzerTest
{
    private readonly Type codeFixProviderType;
    private readonly string sourceNodeName;
    private readonly string targetNodeName;
    private readonly ImmutableArray<int> diagnosticParameterIndexes;
    private readonly string expectedCodeFixTitle;
    private readonly int codeFixParameterIndex;

    public override Type AnalyzerType => typeof(NullCheckAnalyzer);

    public override Type CodeFixProviderType => codeFixProviderType;

    public NullChecksAnalyzerTest(
        Type codeFixProviderType,
        string sourceNodeName,
        string targetNodeName,
        int[] diagnosticParameterIndexes,
        string expectedCodeFixTitle,
        int codeFixParameterIndex)
    {
        this.codeFixProviderType = codeFixProviderType;
        this.sourceNodeName = sourceNodeName;
        this.targetNodeName = targetNodeName;
        this.diagnosticParameterIndexes = diagnosticParameterIndexes.ToImmutableArray();
        this.expectedCodeFixTitle = expectedCodeFixTitle;
        this.codeFixParameterIndex = codeFixParameterIndex;
    }

    public override MethodDeclarationSyntax GetSourceNode(SyntaxNode root)
    {
        return root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(syntax => syntax.Identifier.Text == sourceNodeName)
            .Single();
    }

    public override MethodDeclarationSyntax GetTargetNode(SyntaxNode root)
    {
        return root.DescendantNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(syntax => syntax.Identifier.Text == targetNodeName)
            .Single();
    }

    public override ImmutableArray<Diagnostic> GetExpectedDiagnostics(MethodDeclarationSyntax sourceNode)
    {
        var parameters = sourceNode
            .DescendantNodes()
            .OfType<ParameterListSyntax>()
            .Single()
            .Parameters;
        var builder = ImmutableArray.CreateBuilder<Diagnostic>();
        foreach (var index in diagnosticParameterIndexes)
        {
            var parameter = parameters[index];
            builder.Add(Diagnostic.Create(NullCheckAnalyzer.Descriptor, parameter.Identifier.GetLocation()));
        }
        return builder.ToImmutable();
    }

    public override bool IsExpectedCodeFix(CodeAction action, ImmutableArray<Diagnostic> diagnostics)
    {
        if (action.Title != expectedCodeFixTitle)
        {
            return false;
        }
        if (diagnostics.Length != 1)
        {
            return false;
        }
        if (diagnostics[0].Properties.TryGetValue(nameof(NullableParameter.Index), out string? index))
        {
            return index is not null && int.Parse(index) == codeFixParameterIndex;
        }
        else
        {
            return false;
        }
    }
}
