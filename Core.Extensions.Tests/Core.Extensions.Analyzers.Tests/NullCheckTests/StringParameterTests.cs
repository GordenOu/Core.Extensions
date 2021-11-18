using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests;

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
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddRequiresNullCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullOrEmptyChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target2),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddRequiresNullOrEmptyCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target3),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddDebugNullCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullOrEmptyChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target4),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddDebugNullOrEmptyCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullOrWhitespaceChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target5),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddRequiresNullOrWhitespaceCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullOrWhitespaceChecksCodeFixProvider),
            sourceNodeName: nameof(StringParameterTestCases.Test1Source),
            targetNodeName: nameof(StringParameterTestCases.Test1Target6),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddDebugNullOrWhitespaceCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);
    }
}
