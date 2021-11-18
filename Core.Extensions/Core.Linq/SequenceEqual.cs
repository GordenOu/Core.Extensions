using System.Diagnostics;
using Core.Diagnostics;

namespace Core.Linq;

/// <summary>
/// Represents the method that compares two objects of the same type for equality.
/// </summary>
public delegate bool EqualityComparison<in T>(T? x, T? y);

internal class EqualityComparer<T> : System.Collections.Generic.EqualityComparer<T>
{
    private readonly EqualityComparison<T> equalityComparison;

    private EqualityComparer(EqualityComparison<T> equalityComparison)
    {
        Debug.Assert(equalityComparison is not null);

        this.equalityComparison = equalityComparison;
    }

    public static IEqualityComparer<T> Create(EqualityComparison<T>? equalityComparison)
    {
        return equalityComparison is null
            ? Default
            : new EqualityComparer<T>(equalityComparison);
    }

    public override bool Equals(T? x, T? y)
    {
        return equalityComparison(x, y);
    }

    public override int GetHashCode(T obj)
    {
        throw new NotImplementedException();
    }
}

public static partial class Enumerable
{
    /// <summary>
    /// Determines whether two sequences have equal length and elements using the default
    /// equality comparer for their element type.
    /// </summary>
    /// <typeparam name="T">The element type of the sequences.</typeparam>
    /// <param name="first">The first sequence.</param>
    /// <param name="second">The second sequence.</param>
    /// <returns>
    /// True if the two sequences have equal length and elements.
    /// </returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="first"/> or <paramref name="second"/> is null.
    /// </exception>
    public static bool SequenceEqual<T>(this IEnumerable<T> first, params T[] second)
    {
        Requires.NotNull(first);
        Requires.NotNull(second);

        return System.Linq.Enumerable.SequenceEqual(first, second);
    }

    /// <summary>
    /// Determines whether two sequences have equal length and elements using the given
    /// function to compare the elements.
    /// </summary>
    /// <typeparam name="T">The element type of the sequences.</typeparam>
    /// <param name="first">The first sequence.</param>
    /// <param name="second">The second sequence.</param>
    /// <param name="equalityComparison">
    /// The function used to compare elements for equality.
    /// </param>
    /// <returns>True if the two sequences have equal length and elements.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="first"/> or <paramref name="second"/> is null.
    /// </exception>
    public static bool SequenceEqual<T>(
        this IEnumerable<T> first,
        IEnumerable<T> second,
        EqualityComparison<T>? equalityComparison)
    {
        Requires.NotNull(first);
        Requires.NotNull(second);

        return System.Linq.Enumerable.SequenceEqual(
            first,
            second,
            EqualityComparer<T>.Create(equalityComparison));
    }
}
