using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests;

public unsafe static class PointerParameterTestCases
{
    public static void Test1Source(int* a)
    {
        Console.WriteLine(*a);
    }

    public static void Test1Target1(int* a)
    {
        Requires.NotNullPtr(a);

        Console.WriteLine(*a);
    }

    public static void Test1Target2(int* a)
    {
        Debug.Assert(a is not null);

        Console.WriteLine(*a);
    }
}
