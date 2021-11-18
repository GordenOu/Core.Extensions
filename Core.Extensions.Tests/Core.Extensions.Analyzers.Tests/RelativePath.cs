using System.IO;
using System.Runtime.CompilerServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests;

internal static class RelativePath
{
    public static string GetFilePath(string fileName, [CallerFilePath] string? callerFilePath = null)
    {
        Assert.IsNotNull(callerFilePath);
        string? basePath = Path.GetDirectoryName(callerFilePath);
        Assert.IsNotNull(basePath);
        string path = Path.Combine(basePath, fileName);
        return path;
    }
}
