using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests;

[TestClass]
public class ObjectParameterTests
{
    private static Project? project;
    private static DocumentId? sourceDocumentId;

    [ClassInitialize]
    public static async Task Initialize(TestContext _)
    {
        string sourceDocumentPath = RelativePath.GetFilePath($"{nameof(ObjectParameterTestCases)}.cs");
        (project, sourceDocumentId) = await Workspace.GetCurrentProjectWithDocumentAsync(sourceDocumentPath);
    }

    [TestMethod]
    public async Task Test1()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test1Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test1Target),
            diagnosticParameterIndexes: new[] { 0, 1 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test1Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test1Target),
            diagnosticParameterIndexes: new[] { 0, 1 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 1);
        await test.Run(project, sourceDocumentId);
    }

    [TestMethod]
    public async Task Test2()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test2Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test2Target),
            diagnosticParameterIndexes: new[] { 1, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 1);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test2Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test2Target),
            diagnosticParameterIndexes: new[] { 1, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);
    }

    [TestMethod]
    public async Task Test3()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test3Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test3Target),
            diagnosticParameterIndexes: new[] { 0, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test3Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test3Target),
            diagnosticParameterIndexes: new[] { 0, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);
    }

    [TestMethod]
    public async Task Test4()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test4Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test4Target),
            diagnosticParameterIndexes: new[] { 0, 1 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test4Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test4Target),
            diagnosticParameterIndexes: new[] { 0, 1 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 1);
        await test.Run(project, sourceDocumentId);
    }

    [TestMethod]
    public async Task Test5()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test5Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test5Target),
            diagnosticParameterIndexes: new[] { 1, 3 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 1);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test5Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test5Target),
            diagnosticParameterIndexes: new[] { 1, 3 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 3);
        await test.Run(project, sourceDocumentId);
    }

    [TestMethod]
    public async Task Test6()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test6Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test6Target),
            diagnosticParameterIndexes: new[] { 1, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 1);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test6Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test6Target),
            diagnosticParameterIndexes: new[] { 1, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);
    }

    [TestMethod]
    public async Task Test7()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test7Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test7Target),
            diagnosticParameterIndexes: new[] { 0, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test7Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test7Target),
            diagnosticParameterIndexes: new[] { 0, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);
    }

    [TestMethod]
    public async Task Test8()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test8Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test8Target),
            diagnosticParameterIndexes: new[] { 0, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test8Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test8Target),
            diagnosticParameterIndexes: new[] { 0, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);
    }

    [TestMethod]
    public async Task Test9()
    {
        var test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddRequiresNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test9Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test9Target1),
            diagnosticParameterIndexes: new[] { 0, 2 },
            expectedCodeFixTitle: Strings.AddRequiresNullChecksTitle,
            codeFixParameterIndex: 0);
        await test.Run(project, sourceDocumentId);

        test = new NullChecksAnalyzerTest(
            codeFixProviderType: typeof(AddDebugNullChecksCodeFixProvider),
            sourceNodeName: nameof(ObjectParameterTestCases.Test9Source),
            targetNodeName: nameof(ObjectParameterTestCases.Test9Target2),
            diagnosticParameterIndexes: new[] { 0, 2 },
            expectedCodeFixTitle: Strings.AddDebugNullChecksTitle,
            codeFixParameterIndex: 2);
        await test.Run(project, sourceDocumentId);
    }
}
