using System;
using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests;

public static class StringParameterTestCases
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

    public static void Test1Target4(int a, object b, string c, double[] d)
    {
        Debug.Assert(b is not null);
        Debug.Assert(!string.IsNullOrEmpty(c));
        Debug.Assert(d is not null);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test1Target5(int a, object b, string c, double[] d)
    {
        Requires.NotNull(b);
        Requires.NotNullOrWhitespace(c);
        Requires.NotNull(d);

        Console.WriteLine((a, b, c, d));
    }

    public static void Test1Target6(int a, object b, string c, double[] d)
    {
        Debug.Assert(b is not null);
        Debug.Assert(!string.IsNullOrWhiteSpace(c));
        Debug.Assert(d is not null);

        Console.WriteLine((a, b, c, d));
    }

    public unsafe static void Test2Source(
        int i1,
        object o1,
        object o2,
        object o3,
        string s1,
        string s2,
        string s3,
        string s4,
        string s5,
        string s6,
        string s7,
        double[] d1,
        double[] d2,
        double[] d3,
        double[] d4,
        byte* p1,
        byte* p2,
        byte* p3)
    {
        Requires.NotNull(o1);
        Debug.Assert(o2 is not null);
        Requires.NotNull(s1);
        Debug.Assert(s2 is not null);
        Requires.NotNullOrEmpty(s3);
        Debug.Assert(!string.IsNullOrEmpty(s4));
        Requires.NotNullOrWhitespace(s5);
        Debug.Assert(!string.IsNullOrWhiteSpace(s6));
        Requires.NotNull(d1);
        Debug.Assert(d2 is not null);
        Requires.NotNullOrEmpty(d3);
        Requires.NotNullPtr(p1);
        Debug.Assert(p2 is not null);

        Console.WriteLine((i1, o1, o2, o3, s1, s2, s3, s4, s5, s6, s7, d1, d2, d3, d4, *p1, *p2, *p3));
    }

    public unsafe static void Test2Target(
        int i1,
        object o1,
        object o2,
        object o3,
        string s1,
        string s2,
        string s3,
        string s4,
        string s5,
        string s6,
        string s7,
        double[] d1,
        double[] d2,
        double[] d3,
        double[] d4,
        byte* p1,
        byte* p2,
        byte* p3)
    {
        Requires.NotNull(o1);
        Debug.Assert(o2 is not null);
        Requires.NotNull(o3);
        Requires.NotNull(s1);
        Debug.Assert(s2 is not null);
        Requires.NotNullOrEmpty(s3);
        Debug.Assert(!string.IsNullOrEmpty(s4));
        Requires.NotNullOrWhitespace(s5);
        Debug.Assert(!string.IsNullOrWhiteSpace(s6));
        Requires.NotNull(s7);
        Requires.NotNull(d1);
        Debug.Assert(d2 is not null);
        Requires.NotNullOrEmpty(d3);
        Requires.NotNull(d4);
        Requires.NotNullPtr(p1);
        Debug.Assert(p2 is not null);
        Requires.NotNullPtr(p3);

        Console.WriteLine((i1, o1, o2, o3, s1, s2, s3, s4, s5, s6, s7, d1, d2, d3, d4, *p1, *p2, *p3));
    }
}
