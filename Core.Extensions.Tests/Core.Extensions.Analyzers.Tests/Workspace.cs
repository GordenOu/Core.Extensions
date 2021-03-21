using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Core.Extensions.Analyzers.Tests
{
    public static class Workspace
    {
        private static readonly AdhocWorkspace currentWorkspace = new();

        public static Project GetCurrentProject()
        {
            var projectId = ProjectId.CreateNewId();
            var executingAssembly = Assembly.GetExecutingAssembly();
            string projectName = executingAssembly.FullName;
            var trustedAssemblies = AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES").ToString()
                .Split(Path.PathSeparator)
                .Select(path => MetadataReference.CreateFromFile(path));
            var referencedAssemblies = executingAssembly
                .GetReferencedAssemblies()
                .Select(Assembly.Load)
                .Select(assembly => MetadataReference.CreateFromFile(assembly.Location));

            var solution = currentWorkspace.CurrentSolution
                .AddProject(projectId, projectName, projectName, LanguageNames.CSharp)
                .AddMetadataReferences(projectId, trustedAssemblies)
                .AddMetadataReferences(projectId, referencedAssemblies)
                .WithProjectCompilationOptions(
                    projectId,
                    new CSharpCompilationOptions(
                        OutputKind.DynamicallyLinkedLibrary,
                        allowUnsafe: true));
            return solution.GetProject(projectId);
        }
    }
}
