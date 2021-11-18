using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Diagnostics.Resources;

namespace Core.Diagnostics;

public static partial class Requires
{
    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the <paramref name="condition"/>
    /// is false.
    /// </summary>
    /// <typeparam name="T">The type of the parameter.</typeparam>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <param name="condition">
    /// True if <paramref name="value"/> is in required range.
    /// </param>
    /// <param name="message">The custom error message.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="condition"/> is false.
    /// </exception>
    [DebuggerStepThrough]
    public static void Range<T>(
        T value,
        bool condition,
        [CallerArgumentExpression("value")] string? paramName = null,
        string? message = null)
    {
        if (!condition)
        {
            throw new ArgumentOutOfRangeException(paramName, value, message);
        }
    }

    private static void Positive<T>(T value, string? paramName)
        where T : IComparable<T>
    {
        if (value.CompareTo(default) <= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, Strings.Positive);
        }
    }

    private static void NonPositive<T>(T value, string? paramName)
        where T : IComparable<T>
    {
        if (value.CompareTo(default) > 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, Strings.NonPositive);
        }
    }

    private static void Negative<T>(T value, string? paramName)
       where T : IComparable<T>
    {
        if (value.CompareTo(default) >= 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, Strings.Negative);
        }
    }

    private static void NonNegative<T>(T value, string? paramName)
      where T : IComparable<T>
    {
        if (value.CompareTo(default) < 0)
        {
            throw new ArgumentOutOfRangeException(paramName, value, Strings.NonNegative);
        }
    }

    #region Positive

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(byte value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<byte>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(decimal value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<decimal>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(double value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<double>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(short value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<short>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(int value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<int>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(long value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<long>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(sbyte value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<sbyte>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(float value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<float>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(ushort value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<ushort>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(uint value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<uint>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero.
    /// </exception>
    [DebuggerStepThrough]
    public static void Positive(ulong value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Positive<ulong>(value, paramName);
    }

    #endregion

    #region NonPositive

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonPositive(decimal value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonPositive<decimal>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonPositive(double value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonPositive<double>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonPositive(short value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonPositive<short>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonPositive(int value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonPositive<int>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonPositive(long value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonPositive<long>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonPositive(sbyte value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonPositive<sbyte>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonPositive(float value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonPositive<float>(value, paramName);
    }

    #endregion

    #region Negative

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void Negative(decimal value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Negative<decimal>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void Negative(double value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Negative<double>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void Negative(short value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Negative<short>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void Negative(int value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Negative<int>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void Negative(long value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Negative<long>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void Negative(sbyte value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Negative<sbyte>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is zero or positive.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is zero or positive.
    /// </exception>
    [DebuggerStepThrough]
    public static void Negative(float value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        Negative<float>(value, paramName);
    }

    #endregion

    #region NonNegative

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonNegative(decimal value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonNegative<decimal>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonNegative(double value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonNegative<double>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonNegative(short value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonNegative<short>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonNegative(int value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonNegative<int>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonNegative(long value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonNegative<long>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonNegative(sbyte value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonNegative<sbyte>(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentOutOfRangeException"/> if the parameter is negative.
    /// </summary>
    /// <param name="value">The value of the parameter.</param>
    /// <param name="paramName">The name of the parameter.</param>
    /// <exception cref="ArgumentOutOfRangeException">
    /// <paramref name="value"/> is negative.
    /// </exception>
    [DebuggerStepThrough]
    public static void NonNegative(float value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        NonNegative<float>(value, paramName);
    }

    #endregion
}
