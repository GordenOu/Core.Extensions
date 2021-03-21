using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
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

string filePath = GetFilePath();
string rootPath = new FileInfo(filePath).Directory.Parent.Parent.FullName;
string dummyProjectPath = Path.Combine(rootPath, "Automation", "Core.Extensions");
string outputPath = Path.Combine(rootPath, "Publish");
if (Directory.Exists(outputPath))
{
    Directory.Delete(outputPath, true);
}
Directory.CreateDirectory(outputPath);

// Build main projects.
foreach (var path in Directory.EnumerateDirectories(Path.Combine(rootPath, "Core.Extensions")))
{
    string projectFilePath = Directory.EnumerateFiles(path, "*.csproj").Single();
    using var stream = File.OpenRead(projectFilePath);
    var document = new XmlDocument();
    document.Load(stream);
    foreach (XmlNode node in document.SelectNodes("/Project/ItemGroup/PackageReference"))
    {
        string packageName = node.Attributes["Include"].Value;
        string version = node.Attributes["Version"].Value;
        Run("dotnet", "add", Path.Combine(dummyProjectPath, "Core.Extensions.csproj"),
            "package", packageName,
            "--version", version);
    }

    Run("dotnet", "pack", path,
        "--configuration", "Release",
        "--include-symbols",
        "-p:SymbolPackageFormat=snupkg",
        "--output", outputPath);
}

// Read entries in project packages.

static List<(string entryName, byte[] data)> GetEntries(IEnumerable<string> packages)
{
    var packageEntries = new List<(string, byte[])>();
    foreach (var package in packages)
    {
        using var archive = ZipFile.OpenRead(package);
        foreach (var entry in archive.Entries)
        {
            if (entry.FullName.StartsWith("lib/"))
            {
                using var stream = entry.Open();
                using var memoryStream = new MemoryStream();
                stream.CopyTo(memoryStream);
                packageEntries.Add((entry.FullName, memoryStream.ToArray()));
            }
        }
    }
    return packageEntries;
}

var packages = from path in Directory.EnumerateFiles(outputPath)
               where path.EndsWith(".nupkg")
               select path;
var packageEntries = GetEntries(packages);
foreach (var package in packages)
{
    File.Delete(package);
}

var symbolPackages = from path in Directory.EnumerateFiles(outputPath)
                     where path.EndsWith(".snupkg")
                     select path;
var symbolPackageEntries = GetEntries(symbolPackages);
foreach (var symbolPackage in symbolPackages)
{
    File.Delete(symbolPackage);
}

// Merge entries into a single package.

static void MergeEntries(string packagePath, List<(string entryName, byte[] data)> entries)
{
    using var outputPackageArchive = ZipFile.Open(packagePath, ZipArchiveMode.Update);
    var dummyEntries = outputPackageArchive.Entries
        .Where(entry => entry.FullName.StartsWith("lib/"))
        .ToList();
    foreach (var entry in dummyEntries)
    {
        entry.Delete();
    }
    foreach (var (entryName, data) in entries)
    {
        var entry = outputPackageArchive.CreateEntry(entryName);
        using var stream = entry.Open();
        stream.Write(data);
        stream.Flush();
    }
}

Run("dotnet", "pack", dummyProjectPath,
    "--configuration", "Release",
    "--include-symbols",
    "-p:SymbolPackageFormat=snupkg",
    "--output", outputPath);
string outputPackagePath = Directory.EnumerateFiles(outputPath)
        .Where(path => path.EndsWith(".nupkg"))
        .Single();
MergeEntries(outputPackagePath, packageEntries);
string outputSymbolPackagePath = Directory.EnumerateFiles(outputPath)
    .Where(path => path.EndsWith(".snupkg"))
    .Single();
MergeEntries(outputSymbolPackagePath, symbolPackageEntries);

Run("dotnet", "pack", Path.Combine(rootPath, "Core.Extensions.Analyzers"),
    "--configuration", "Release",
    "--output", outputPath);
