using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests
{
    public abstract class AnalyzerTest
    {
        public abstract Type AnalyzerType { get; }

        public abstract Type CodeFixProviderType { get; }

        public abstract Diagnostic[] GetExpectedDiagnostics(SyntaxNode root);

        public abstract bool IsExpectedCodeFix(CodeAction action);

        public abstract SyntaxNode GetFixedNode(SyntaxNode root);

        public async Task Run(
            Project project,
            string sourceFileName,
            string sourceText,
            string targetFileName,
            string targetText)
        {
            var sourceDocumentId = DocumentId.CreateNewId(project.Id);
            var targetDocumentId = DocumentId.CreateNewId(project.Id);
            var solution = project.Solution
                .AddDocument(sourceDocumentId, sourceFileName, sourceText)
                .AddDocument(targetDocumentId, targetFileName, targetText);
            var compilation = await solution.GetProject(project.Id).GetCompilationAsync();
            var diagnostics = compilation.GetDiagnostics();
            Assert.AreEqual(0, diagnostics.Length);

            var analyzer = (DiagnosticAnalyzer)Activator.CreateInstance(AnalyzerType);
            var compilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create(analyzer));
            var root = await solution.GetDocument(sourceDocumentId).GetSyntaxRootAsync();
            var expectedDiagnostics = GetExpectedDiagnostics(root);
            var analyzerDiagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
            CollectionAssert.AreEquivalent(expectedDiagnostics, analyzerDiagnostics);

            var codeFixProvider = (CodeFixProvider)Activator.CreateInstance(CodeFixProviderType);
            var codeFixes = new List<CodeAction>();
            var codeFixContext = new CodeFixContext(
                solution.GetDocument(sourceDocumentId),
                analyzerDiagnostics[0],
                (action, _) =>
                {
                    if (IsExpectedCodeFix(action))
                    {
                        codeFixes.Add(action);
                    }
                },
                default);
            await codeFixProvider.RegisterCodeFixesAsync(codeFixContext);
            Assert.AreEqual(1, codeFixes.Count);
            var operations = await codeFixes[0].GetOperationsAsync(default);
            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(ApplyChangesOperation));

            var targetDocument = solution.GetDocument(targetDocumentId);
            var targetRoot = await targetDocument.GetSyntaxRootAsync();
            var expectedFixedNode = GetFixedNode(targetRoot)
                .ToFullString()
                .Replace("\r\n", "\n")
                .Replace("\r", "\n");;
            var operation = (ApplyChangesOperation)operations[0];
            var fixedDocument = operation.ChangedSolution.GetDocument(sourceDocumentId);
            var fixedRoot = await fixedDocument.GetSyntaxRootAsync();
            var fixedNode = GetFixedNode(fixedRoot)
                .ToFullString()
                .Replace("\r\n", "\n")
                .Replace("\r", "\n");;
            Assert.AreEqual(expectedFixedNode, fixedNode);
        }
    }
}
