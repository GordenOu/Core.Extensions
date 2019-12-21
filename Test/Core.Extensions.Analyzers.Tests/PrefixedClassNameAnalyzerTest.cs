using System.IO;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Core.Extensions.Analyzers.Tests
{
    public abstract class PrefixedClassNameAnalyzerTest : AnalyzerTest
    {
        public virtual async Task Test([CallerFilePath] string filePath = null)
        {
            string basePath = Path.GetDirectoryName(filePath);
            string sourceFileName = $"{GetType().Name}Source.cs";
            string targetFileName = $"{GetType().Name}Target.cs";
            string sourceText = await File.ReadAllTextAsync(Path.Combine(basePath, sourceFileName));
            string targetText = await File.ReadAllTextAsync(Path.Combine(basePath, targetFileName));

            await Run(
                Workspace.GetCurrentProject(),
                sourceFileName,
                sourceText,
                targetFileName,
                targetText);
        }
    }
}
