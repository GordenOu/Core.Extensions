using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests;

[TestClass]
public class PointerParameterTests
{
    private static Project? project;
    private static DocumentId? sourceDocumentId;

    [ClassInitialize]
    public static async Task Initialize(TestContext _)
    {
        string sourceDocumentPath = RelativePath.GetFilePath($"{nameof(PointerParameterTestCases)}.cs");
        (project, sourceDocumentId) = await Workspace.GetCurrentProjectWithDocumentAsync(sourceDocumentPath);
    }
    
    [TestMethod]
    public async Task Test1()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(PointerParameterTestCases.Test1Source),
            targetNodeName: nameof(PointerParameterTestCases.Test1Target1),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddRequiresNullCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullChecksCodeFixProvider),
            sourceNodeName: nameof(PointerParameterTestCases.Test1Source),
            targetNodeName: nameof(PointerParameterTestCases.Test1Target2),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddDebugNullCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);
    }
}
