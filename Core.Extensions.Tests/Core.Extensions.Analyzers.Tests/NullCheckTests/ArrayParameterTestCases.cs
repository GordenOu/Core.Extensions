using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests;

public static class ArrayParameterTestCases
{
    public static void Test1Source(int[] a)
    {
        Console.WriteLine(a);
    }

    public static void Test1Target1(int[] a)
    {
        Requires.NotNull(a);

        Console.WriteLine(a);
    }

    public static void Test1Target2(int[] a)
    {
        Requires.NotNullOrEmpty(a);

        Console.WriteLine(a);
    }

    public static void Test1Target3(int[] a)
    {
        Debug.Assert(a is not null);

        Console.WriteLine(a);
    }
}
