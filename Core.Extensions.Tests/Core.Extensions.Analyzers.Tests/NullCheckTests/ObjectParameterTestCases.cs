using System;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests;

public static class ObjectParameterTestCases
{
    public static void Test1Source(object a)
    {
        Console.WriteLine(a);
    }

    public static void Test1Target(object a)
    {
        Requires.NotNull(a);

        Console.WriteLine(a);
    }

    public static void Test2Source(bool a, object b, int c)
    {
        Console.WriteLine((a, b, c));
    }

    public static void Test2Target(bool a, object b, int c)
    {
        Requires.NotNull(b);

        Console.WriteLine((a, b, c));
    }

    public static void Test3Source(object a, object b, object c)
    {
        Requires.NotNull(a);

        Console.WriteLine((a, b, c));
    }

    public static void Test3Target1(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);

        Console.WriteLine((a, b, c));
    }

    public static void Test3Target2(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test4Source(object a, object b, object c)
    {
        Requires.NotNull(b);

        Console.WriteLine((a, b, c));
    }

    public static void Test4Target1(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);

        Console.WriteLine((a, b, c));
    }

    public static void Test4Target2(object a, object b, object c)
    {
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test5Source(object a, object b, object c)
    {
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test5Target1(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test5Target2(object a, object b, object c)
    {
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test6Source(object a, object b, object c)
    {
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test6Target(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test7Source(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test7Target(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test8Source(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);

        Console.WriteLine((a, b, c));
    }

    public static void Test8Target(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test9Source(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(a);
        Requires.NotNull(c);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test9Target(object a, object b, object c)
    {
        Requires.NotNull(a);
        Requires.NotNull(a);
        Requires.NotNull(b);
        Requires.NotNull(c);
        Requires.NotNull(c);

        Console.WriteLine((a, b, c));
    }

    public static void Test10Source(object a, object b, object c)
    {
        Requires.NotNull(c);
        Requires.NotNull(a);

        Console.WriteLine((a, b, c));
    }

    public static void Test10Target(object a, object b, object c)
    {
        Requires.NotNull(b);
        Requires.NotNull(c);
        Requires.NotNull(a);

        Console.WriteLine((a, b, c));
    }

    public static void Test11Source(object a, object b, object c)
    {
        Requires.NotNull(b);
        Requires.NotNull(a);

        Console.WriteLine((a, b, c));
    }

    public static void Test11Target(object a, object b, object c)
    {
        Requires.NotNull(c);
        Requires.NotNull(b);
        Requires.NotNull(a);

        Console.WriteLine((a, b, c));
    }
}
