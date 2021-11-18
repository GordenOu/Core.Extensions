using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests;

public static class ArrayParameterTestCases
{
    public static void Test1Source(int a, object b, string c, double[] d)
    {
        Console.WriteLine((a, b, c, d));
    }

    public static void Test1Target1(int a, object b, string c, double[] d)
    {
        Requires.NotNull(b);
        Requires.NotNull(c);
        Requires.NotNull(d);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test1Target2(int a, object b, string c, double[] d)
    {
        Requires.NotNull(b);
        Requires.NotNullOrEmpty(c);
        Requires.NotNullOrEmpty(d);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test1Target3(int a, object b, string c, double[] d)
    {
        Debug.Assert(b is not null);
        Debug.Assert(c is not null);
        Debug.Assert(d is not null);

        Console.WriteLine((a, b, c, d));
    }
}
