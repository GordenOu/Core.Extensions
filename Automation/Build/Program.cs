using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DotNet.Cli.Utils;

static string GetFilePath([CallerFilePath] string path = null)
{
    return path;
}

static void Run(string commandName, params string[] args)
{
    var command = Command.Create(commandName, args);
    Console.WriteLine("Command:");
    Console.WriteLine($"{command.CommandName} {command.CommandArgs}");
    Console.WriteLine();
    var result = command.Execute();
    if (result.ExitCode != 0)
    {
        throw new InvalidOperationException(nameof(result.ExitCode));
    }
    Console.WriteLine();
}

static void BuildProject(string path)
{
    string projectFilePath = Directory.EnumerateFiles(path, "*.csproj").Single();
    Run("dotnet", "build", projectFilePath);
}

string filePath = GetFilePath();
string rootPath = new FileInfo(filePath).Directory.Parent.Parent.FullName;
foreach (var path in Directory.EnumerateDirectories(Path.Combine(rootPath, "Core.Extensions")))
{
    BuildProject(path);
}
foreach (var path in Directory.EnumerateDirectories(Path.Combine(rootPath, "Core.Extensions.Tests")))
{
    BuildProject(path);
}
