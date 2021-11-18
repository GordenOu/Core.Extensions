using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests;

public static class GenericParameterTestCases
{
    public static void Test1Source<T1, T2>(object a, T1 b, T2 c)
        where T2 : class
    {
        Console.WriteLine((a, b, c));
    }

    public static void Test1Target1<T1, T2>(object a, T1 b, T2 c)
        where T2 : class
    {
        Requires.NotNull(a);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test1Target2<T1, T2>(object a, T1 b, T2 c)
        where T2 : class
    {
        Debug.Assert(a is not null);
        Debug.Assert(c is not null);

        Console.WriteLine((a, b, c));
    }
}
