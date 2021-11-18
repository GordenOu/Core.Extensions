using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests;

[TestClass]
public class GenericParameterTests
{
    private static Project? project;
    private static DocumentId? sourceDocumentId;

    [ClassInitialize]
    public static async Task Initialize(TestContext _)
    {
        string sourceDocumentPath = RelativePath.GetFilePath($"{nameof(GenericParameterTestCases)}.cs");
        (project, sourceDocumentId) = await Workspace.GetCurrentProjectWithDocumentAsync(sourceDocumentPath);
    }
    
    [TestMethod]
    public async Task Test1()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(GenericParameterTestCases.Test1Source),
            targetNodeName: nameof(GenericParameterTestCases.Test1Target1),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddRequiresNullCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullChecksCodeFixProvider),
            sourceNodeName: nameof(GenericParameterTestCases.Test1Source),
            targetNodeName: nameof(GenericParameterTestCases.Test1Target2),
            diagnosticParameterIndexes: new[] { 0 },
            expectedCodeFixTitle: Strings.AddDebugNullCheckTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);
    }
}
