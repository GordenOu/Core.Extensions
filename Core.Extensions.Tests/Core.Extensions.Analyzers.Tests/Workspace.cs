using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests;

[TestClass]
public static class Workspace
{
    private static AdhocWorkspace? workspace;
    private static Project? currentProject;

    [AssemblyInitialize]
    public static void Initialize(TestContext _)
    {
        workspace = new AdhocWorkspace();
    }

    [AssemblyCleanup]
    public static void Cleanup()
    {
        workspace?.Dispose();
    }

    public static Project GetCurrentProject()
    {
        Assert.IsNotNull(workspace);
        if (currentProject is null)
        {
            var projectId = ProjectId.CreateNewId();
            var executingAssembly = Assembly.GetExecutingAssembly();
            string? projectName = executingAssembly.FullName;
            Assert.IsNotNull(projectName);
            var trustedAssemblies = AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")!.ToString()!
                .Split(Path.PathSeparator)
                .Select(path => MetadataReference.CreateFromFile(path));
            var referencedAssemblies = executingAssembly
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Select(assembly => MetadataReference.CreateFromFile(assembly.Location));

            var solution = workspace.CurrentSolution
                .AddProject(projectId, projectName, projectName, LanguageNames.CSharp)
                .AddMetadataReferences(projectId, trustedAssemblies)
                .AddMetadataReferences(projectId, referencedAssemblies)
                .WithProjectCompilationOptions(
                    projectId,
                    new CSharpCompilationOptions(
                        OutputKind.DynamicallyLinkedLibrary,
                        nullableContextOptions: NullableContextOptions.Enable,
                        allowUnsafe: true));
            var project = solution.GetProject(projectId);
            Assert.IsNotNull(project);
            currentProject = project;
        }
        return currentProject;
    }

    public static async Task<(Project, DocumentId)> GetCurrentProjectWithDocumentAsync(string documentPath)
    {
        var project = GetCurrentProject();

        string documentText = await File.ReadAllTextAsync(documentPath);
        var sourceDocumentId = DocumentId.CreateNewId(project.Id);
        var solution = project.Solution.AddDocument(sourceDocumentId, documentPath, documentText);
        project = solution.GetProject(project.Id);
        Assert.IsNotNull(project);

        return (project, sourceDocumentId);
    }
}
