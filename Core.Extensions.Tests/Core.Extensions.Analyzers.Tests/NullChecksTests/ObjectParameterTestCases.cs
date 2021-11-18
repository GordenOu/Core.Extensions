using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests;

public static class ObjectParameterTestCases
{
    public static void Test1Source(object a, object b)
    {
        Console.WriteLine((a, b));
    }

    public static void Test1Target(object a, object b)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);

        Console.WriteLine((a, b));
    }

    public static void Test2Source(object a, object b, object c)
    {
        Requires.NotNull(a);

        Console.WriteLine((a, b, c));
    }

    public static void Test2Target(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test3Source(object a, object b, object c)
    {
        Requires.NotNull(b);

        Console.WriteLine((a, b, c));
    }

    public static void Test3Target(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test4Source(object a, object b, object c)
    {
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test4Target(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test5Source(object a, object b, object c, object d)
    {
        Requires.NotNull(a);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test5Target(object a, object b, object c, object d)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);
        Requires.NotNull(d);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test6Source(object a, object b, object c, object d)
    {
        Requires.NotNull(a);
        Requires.NotNull(d);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test6Target(object a, object b, object c, object d)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);
        Requires.NotNull(d);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test7Source(object a, object b, object c, object d)
    {
        Requires.NotNull(b);
        Requires.NotNull(d);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test7Target(object a, object b, object c, object d)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);
        Requires.NotNull(d);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test8Source(object a, object b, object c, object d)
    {
        Requires.NotNull(d);
        Requires.NotNull(b);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test8Target(object a, object b, object c, object d)
    {
        Requires.NotNull(a);
        Requires.NotNull(c);
        Requires.NotNull(d);
        Requires.NotNull(b);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test9Source(object a, object? b, object c)
    {
        Console.WriteLine((a, b, c));
    }

    public static void Test9Target1(object a, object? b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test9Target2(object a, object? b, object c)
    {
        Debug.Assert(a is not null);
        Debug.Assert(c is not null);

        Console.WriteLine((a, b, c));
    }
}
