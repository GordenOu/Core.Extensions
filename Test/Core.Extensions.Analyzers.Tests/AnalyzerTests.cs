using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CodeFixes;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests
{
    [TestClass]
    public class AnalyzerTests
    {
        private static string GetFilePath([CallerFilePath]string path = null)
        {
            return path;
        }

        [DataRow(
            nameof(NullCheckTest1Source),
            nameof(NullCheckTest1Target),
            typeof(NullCheckAnalyzer),
            typeof(AddNullCheckCodeFixProvider))]
        [DataTestMethod]
        public async Task TestAnalyzers(
            string sourceName,
            string targetName,
            Type diagnosticAnalyzerType,
            Type codeFixProviderType)
        {
            string sourceFileName = sourceName + ".cs";
            string targetFileName = targetName + ".cs";
            string path = Path.GetDirectoryName(GetFilePath());
            string sourceText = await File.ReadAllTextAsync(Path.Combine(path, sourceFileName));
            string targetText = await File.ReadAllTextAsync(Path.Combine(path, targetFileName));
            var analyzer = (DiagnosticAnalyzer)Activator.CreateInstance(diagnosticAnalyzerType);

            var project = Workspace.GetProject();
            var sourceDocumentId = DocumentId.CreateNewId(project.Id);
            var targetDocumentId = DocumentId.CreateNewId(project.Id);
            var solution = project.Solution
                .AddDocument(sourceDocumentId, sourceFileName, sourceText)
                .AddDocument(targetDocumentId, targetFileName, targetText);
            var compilation = await solution.GetProject(project.Id).GetCompilationAsync();
            var diagnostics = compilation.GetDiagnostics();
            Assert.AreEqual(0, diagnostics.Length);

            var compilationWithAnalyzers = compilation.WithAnalyzers(ImmutableArray.Create(analyzer));
            var analyzerDiagnostics = await compilationWithAnalyzers.GetAnalyzerDiagnosticsAsync();
            Assert.AreEqual(1, analyzerDiagnostics.Length);
            Assert.AreEqual(analyzer.SupportedDiagnostics.Single().Id, analyzerDiagnostics[0].Id);

            var codeFixProvider = (CodeFixProvider)Activator.CreateInstance(codeFixProviderType);
            var actions = new List<CodeAction>();
            var codeFixContext = new CodeFixContext(
                solution.GetDocument(sourceDocumentId),
                analyzerDiagnostics[0],
                (action, _) => actions.Add(action),
                default);
            await codeFixProvider.RegisterCodeFixesAsync(codeFixContext);

            Assert.AreEqual(1, actions.Count);
            var operations = await actions[0].GetOperationsAsync(default);
            Assert.AreEqual(1, operations.Length);
            Assert.IsInstanceOfType(operations[0], typeof(ApplyChangesOperation));
            var operation = (ApplyChangesOperation)operations[0];
            var fixedDocument = operation.ChangedSolution.GetDocument(sourceDocumentId);
            var fixedRoot = await fixedDocument.GetSyntaxRootAsync();
            var fixMethod = fixedRoot.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Single()
                .ToFullString()
                .Replace("\r\n", "\n")
                .Replace("\r", "\n");
            var targetDocument = solution.GetDocument(targetDocumentId);
            var targetRoot = await targetDocument.GetSyntaxRootAsync();
            var targetMethod = targetRoot.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Single()
                .ToFullString()
                .Replace("\r\n", "\n")
                .Replace("\r", "\n");

            Assert.AreEqual(targetMethod, fixMethod);
        }
    }
}
