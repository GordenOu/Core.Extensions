using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests;

[TestClass]
public class StringParameterTests
{
    private static Project? project;
    private static DocumentId? sourceDocumentId;

    [ClassInitialize]
    public static async Task Initialize(TestContext _)
    {
        string sourceDocumentPath = RelativePath.GetFilePath($"{nameof(StringParameterTestCases)}.cs");
        (project, sourceDocumentId) = await Workspace.GetCurrentProjectWithDocumentAsync(sourceDocumentPath);
    }

    [TestMethod]
    public async Task Test1()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target1),
            diagnosticParameterIndexes: new[] { 1, 2, 3 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullOrEmptyChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target2),
            diagnosticParameterIndexes: new[] { 1, 2, 3 },
            expectedCodeFixTitle: Strings.AddRequiresNullOrEmptyChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target3),
            diagnosticParameterIndexes: new[] { 1, 2, 3 },
            expectedCodeFixTitle: Strings.AddDebugNullChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullOrEmptyChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target4),
            diagnosticParameterIndexes: new[] { 1, 2, 3 },
            expectedCodeFixTitle: Strings.AddDebugNullOrEmptyChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullOrWhitespaceChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target5),
            diagnosticParameterIndexes: new[] { 1, 2, 3 },
            expectedCodeFixTitle: Strings.AddRequiresNullOrWhitespaceChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullOrWhitespaceChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target6),
            diagnosticParameterIndexes: new[] { 1, 2, 3 },
            expectedCodeFixTitle: Strings.AddDebugNullOrWhitespaceChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);
    }


    [TestMethod]
    public async Task Test2()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test2Source),
            targetNodeName: nameof(StringParameterTestCases.Test2Target),
            diagnosticParameterIndexes: new[] { 3, 10, 14, 17 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 10);
        await test.Run(project, sourceDocumentId);
    }
}
