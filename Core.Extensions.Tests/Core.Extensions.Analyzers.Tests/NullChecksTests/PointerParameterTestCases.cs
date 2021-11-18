using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests;

public unsafe class PointerParameterTestCases
{
    public static void Test1Source(object a, int b, int* c)
    {
        Console.WriteLine((a, b, *c));
    }

    public static void Test1Target1(object a, int b, int* c)
    {
        Requires.NotNull(a);
        Requires.NotNullPtr(c);

        Console.WriteLine((a, b, *c));
    }

    public static void Test1Target2(object a, int b, int* c)
    {
        Debug.Assert(a is not null);
        Debug.Assert(c is not null);

        Console.WriteLine((a, b, *c));
    }
}
