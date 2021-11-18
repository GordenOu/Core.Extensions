using System.Runtime.CompilerServices;
using Microsoft.DotNet.Cli.Utils;

static string GetFilePath([CallerFilePath] string? path = null)
{
    if (path is null)
    {
        throw new InvalidOperationException(nameof(path));
    }
    return path;
}

string filePath = GetFilePath();
string? rootPath = new FileInfo(filePath).Directory?.Parent?.Parent?.FullName;
if (rootPath is null)
{
    throw new InvalidOperationException(nameof(rootPath));
}
var testProjectFiles = new DirectoryInfo(Path.Combine(rootPath, "Core.Extensions.Tests"))
   .EnumerateDirectories("Core.*.Tests", SearchOption.TopDirectoryOnly)
   .Select(directory => directory.EnumerateFiles("*.csproj").Single());
var exitCodes = new List<int>();
foreach (var file in testProjectFiles)
{
    var command = Command.CreateDotNet("test", new[] { file.FullName });
    Console.WriteLine("Command:");
    Console.WriteLine($"{command.CommandName} {command.CommandArgs}");
    Console.WriteLine();
    var result = command.Execute();
    exitCodes.Add(result.ExitCode);
}
if (exitCodes.Any(exitCode => exitCode != 0))
{
    Environment.Exit(1);
}
