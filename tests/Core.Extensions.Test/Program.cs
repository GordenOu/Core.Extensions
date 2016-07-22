using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;

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
            string testFileName = Path.Combine(Path.GetTempPath(), "test.cmd");
            File.WriteAllText(testFileName, "dotnet test");

            var passed = new FileInfo(GetFilePath())
                .Directory
                .Parent
                .EnumerateDirectories("Core*Tests", SearchOption.TopDirectoryOnly)
                .SelectMany(directory =>
                {
                    var process = new Process
                    {
                        StartInfo = new ProcessStartInfo
                        {
                            FileName = testFileName,
                            WorkingDirectory = directory.FullName,
                            RedirectStandardOutput = true
                        }
                    };
                    process.Start();
                    process.WaitForExit();

                    return process.StandardOutput.ReadToEnd()
                    .Split(new[] { Environment.NewLine }, StringSplitOptions.None)
                    .Select(line =>
                    {
                        Console.WriteLine(line);
                        return !line.StartsWith("Failed  ");
                    });
                });
            if (passed.Count(false.Equals) != 0 && args?.Length != 0)
            {
                throw new Exception("Test Failed.");
            }
        }
    }
}
