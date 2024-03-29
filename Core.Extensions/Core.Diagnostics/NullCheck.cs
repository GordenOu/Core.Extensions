﻿using System.Diagnostics;
using System.Runtime.CompilerServices;
using Core.Diagnostics.Resources;

namespace Core.Diagnostics;

public static partial class Requires
{
    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the parameter is null.
    /// </summary>
    /// <typeparam name="T">
    /// The type of the parameter.
    /// </typeparam>
    /// <param name="value">
    /// The value of the parameter.
    /// </param>
    /// <param name="paramName">
    /// The name of the parameter.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="value"/> is null.
    /// </exception>
    [DebuggerStepThrough]
    public static void NotNull<T>(T? value, [CallerArgumentExpression("value")] string? paramName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(value, paramName);
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the pointer parameter is null.
    /// </summary>
    /// <param name="value">
    /// The value of the pointer parameter.
    /// </param>
    /// <param name="paramName">
    /// The name of the pointer parameter.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="value"/> is null.
    /// </exception>
    [DebuggerStepThrough]
    public static unsafe void NotNullPtr(void* value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the collection parameter is null, and
    /// throws <see cref="ArgumentException"/> if the collection parameter is empty.
    /// </summary>
    /// <typeparam name="T">
    /// The element type of the collection parameter.
    /// </typeparam>
    /// <param name="value">
    /// The value of the collection parameter.
    /// </param>
    /// <param name="paramName">
    /// The name of the collection parameter.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="value"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is empty.
    /// </exception>
    [DebuggerStepThrough]
    public static void NotNullOrEmpty<T>(
        IEnumerable<T>? value,
        [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName, Strings.NonEmptyCollection);
        }
        if (!value.Any())
        {
            throw new ArgumentException(Strings.NonEmptyCollection, paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the string parameter is null, and throws
    /// <see cref="ArgumentException"/> if the string parameter is empty.
    /// </summary>
    /// <param name="value">
    /// The value of the string parameter.
    /// </param>
    /// <param name="paramName">
    /// The name of the string parameter.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="value"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is empty.
    /// </exception>
    [DebuggerStepThrough]
    public static void NotNullOrEmpty(string? value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName, Strings.NonEmptyString);
        }
        if (value.Length == 0)
        {
            throw new ArgumentException(Strings.NonEmptyString, paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentNullException"/> if the string parameter is null, and throws
    /// <see cref="ArgumentException"/> if the string parameter is empty, or consists only of white-space
    /// characters.
    /// </summary>
    /// <param name="value">
    /// The value of the string parameter.
    /// </param>
    /// <param name="paramName">
    /// The name of the string parameter.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="value"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> is empty, or consists only of white-space characters.
    /// </exception>
    [DebuggerStepThrough]
    public static void NotNullOrWhitespace(string? value, [CallerArgumentExpression("value")] string? paramName = null)
    {
        if (value is null)
        {
            throw new ArgumentNullException(paramName, Strings.NonWhitespaceString);
        }
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException(Strings.NonWhitespaceString, paramName);
        }
    }

    /// <summary>
    /// Throws <see cref="ArgumentException"/> if the collection parameter contains null items.
    /// </summary>
    /// <typeparam name="T">
    /// The element type of the collection parameter.
    /// </typeparam>
    /// <param name="value">
    /// The value of the collection parameter.
    /// </param>
    /// <param name="paramName">
    /// The name of the collection parameter.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="value"/> is null.
    /// </exception>
    /// <exception cref="ArgumentException">
    /// <paramref name="value"/> contains null items.
    /// </exception>
    [DebuggerStepThrough]
    public static void NotNullItems<T>(
        IEnumerable<T>? value,
        [CallerArgumentExpression("value")] string? paramName = null)
        where T : class
    {
        ArgumentNullException.ThrowIfNull(value);
        if (value.Any(item => item is null))
        {
            throw new ArgumentException(Strings.NonNullItems, paramName);
        }
    }
}
