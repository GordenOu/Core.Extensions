using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests;

public abstract class AnalyzerTest
{
    public abstract Type AnalyzerType { get; }

    public abstract Type CodeFixProviderType { get; }

    public abstract MethodDeclarationSyntax GetSourceNode(SyntaxNode root);

    public abstract MethodDeclarationSyntax GetTargetNode(SyntaxNode root);

    public abstract ImmutableArray<Diagnostic> GetExpectedDiagnostics(MethodDeclarationSyntax sourceNode);

    public abstract bool IsExpectedCodeFix(CodeAction action, ImmutableArray<Diagnostic> diagnostics);

    private async Task<ImmutableArray<Diagnostic>> GetAnalyzerDiagnosticsAsync(
        Compilation compilation,
        SyntaxTree tree,
        TextSpan filterSpan)
    {
        var analyzer = (DiagnosticAnalyzer?)Activator.CreateInstance(AnalyzerType);
        Assert.IsNotNull(analyzer);
        var compilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create(analyzer));

        var model = compilationWithAnalyzers.Compilation.GetSemanticModel(tree);
        var analyzerDiagnostics = await compilationWithAnalyzers.GetAnalyzerSemanticDiagnosticsAsync(
            model,
            filterSpan,
            default);
        return analyzerDiagnostics;
    }

    private async Task<CodeAction[]> GetExpectedCodeFixesAsync(
        Document sourceDocument,
        IEnumerable<Diagnostic> diagnostics)
    {
        var codeFixProvider = (CodeFixProvider?)Activator.CreateInstance(CodeFixProviderType);
        Assert.IsNotNull(codeFixProvider);
        var codeFixes = new List<CodeAction>();
        foreach (var diagnostic in diagnostics)
        {
            var codeFixContext = new CodeFixContext(
                sourceDocument,
                diagnostic,
                (action, diagnostics) =>
                {
                    if (IsExpectedCodeFix(action, diagnostics))
                    {
                        codeFixes.Add(action);
                    }
                },
                default);
            await codeFixProvider.RegisterCodeFixesAsync(codeFixContext);
        }
        return codeFixes.ToArray();
    }

    public async Task Run(Project? project, DocumentId? sourceDocumentId)
    {
        Assert.IsNotNull(project);
        Assert.IsNotNull(sourceDocumentId);

        var compilation = await project.GetCompilationAsync();
        Assert.IsNotNull(compilation);
        var diagnostics = compilation.GetDiagnostics();
        Assert.AreEqual(0, diagnostics.Length, diagnostics.FirstOrDefault()?.ToString());

        var sourceDocument = project.GetDocument(sourceDocumentId);
        Assert.IsNotNull(sourceDocument);
        var tree = await sourceDocument.GetSyntaxTreeAsync();
        Assert.IsNotNull(tree);
        var root = await tree.GetRootAsync();
        var sourceNode = GetSourceNode(root);
        var targetNode = GetTargetNode(root);

        var expectedDiagnostics = GetExpectedDiagnostics(sourceNode);
        var analyzerDiagnostics = await GetAnalyzerDiagnosticsAsync(compilation, tree, sourceNode.FullSpan);
        CollectionAssert.AreEquivalent(expectedDiagnostics, analyzerDiagnostics);

        var codeFixes = await GetExpectedCodeFixesAsync(sourceDocument, analyzerDiagnostics);
        Assert.AreEqual(1, codeFixes.Length);
        var operations = await codeFixes[0].GetOperationsAsync(default);
        Assert.AreEqual(1, operations.Length);
        Assert.IsInstanceOfType(operations[0], typeof(ApplyChangesOperation));

        var expectedFixedNode = targetNode
            .WithoutTrivia()
            .ToFullString()
            .Replace("\r\n", "\n")
            .Replace("\r", "\n");
        var operation = (ApplyChangesOperation)operations[0];
        var fixedDocument = operation.ChangedSolution.GetDocument(sourceDocumentId);
        Assert.IsNotNull(fixedDocument);
        var fixedRoot = await fixedDocument.GetSyntaxRootAsync();
        Assert.IsNotNull(fixedRoot);
        var fixedNode = GetSourceNode(fixedRoot)
            .WithIdentifier(SyntaxFactory.Identifier(targetNode.Identifier.Text))
            .WithoutTrivia()
            .ToFullString()
            .Replace("\r\n", "\n")
            .Replace("\r", "\n")
            .Trim();
        Assert.AreEqual(expectedFixedNode, fixedNode);
    }
}
