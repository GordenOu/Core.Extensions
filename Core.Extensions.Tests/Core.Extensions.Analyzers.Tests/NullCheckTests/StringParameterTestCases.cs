using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests;

public static class StringParameterTestCases
{
    public static void Test1Source(string a)
    {
        Console.WriteLine(a);
    }

    public static void Test1Target1(string a)
    {
        Requires.NotNull(a);

        Console.WriteLine(a);
    }

    public static void Test1Target2(string a)
    {
        Requires.NotNullOrEmpty(a);

        Console.WriteLine(a);
    }

    public static void Test1Target3(string a)
    {
        Debug.Assert(a is not null);

        Console.WriteLine(a);
    }

    public static void Test1Target4(string a)
    {
        Debug.Assert(!string.IsNullOrEmpty(a));

        Console.WriteLine(a);
    }

    public static void Test1Target5(string a)
    {
        Requires.NotNullOrWhitespace(a);

        Console.WriteLine(a);
    }

    public static void Test1Target6(string a)
    {
        Debug.Assert(!string.IsNullOrWhiteSpace(a));

        Console.WriteLine(a);
    }
}
