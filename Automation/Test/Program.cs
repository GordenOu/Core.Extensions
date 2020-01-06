using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DotNet.Cli.Utils;

namespace Test
{
    class Program
    {
        private static string GetFilePath([CallerFilePath]string path = null)
        {
            return path;
        }

        static void Main()
        {
            string filePath = GetFilePath();
            string rootPath = new FileInfo(filePath).Directory.Parent.Parent.FullName;
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
        }
    }
}
