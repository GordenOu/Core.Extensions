using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.DotNet.Cli.Utils;

namespace Core.Extensions
{
    public class Program
    {
        private static string GetFilePath([CallerFilePath] string filePath = null)
        {
            return filePath;
        }

        public static void Main(string[] args)
        {
            var testProjectFiles = new FileInfo(GetFilePath())
                .Directory
                .Parent
                .EnumerateDirectories("Core.*.Tests", SearchOption.TopDirectoryOnly)
                .SelectMany(directory => directory.EnumerateFiles("*.csproj"));
            bool passed = true;
            foreach (var file in testProjectFiles)
            {
                Console.WriteLine();
                Command.CreateDotNet("test", new[] { file.FullName })
                    .OnErrorLine(line => passed = false)
                    .Execute();
            }
            if (!passed)
            {
                Environment.Exit(1);
            }
        }
    }
}
