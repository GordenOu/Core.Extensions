using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests;

public static class GenericParameterTestCases
{
    public static void Test1Source<T>(T a)
        where T : class
    {
        Console.WriteLine(a);
    }

    public static void Test1Target1<T>(T a)
        where T : class
    {
        Requires.NotNull(a);

        Console.WriteLine(a);
    }

    public static void Test1Target2<T>(T a)
        where T : class
    {
        Debug.Assert(a is not null);

        Console.WriteLine(a);
    }
}
