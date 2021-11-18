using Core.Diagnostics;

namespace Core.Linq;

public static partial class Enumerable
{
    /// <summary>
    /// Applies an action to each of the elements in the source collection.
    /// </summary>
    /// <typeparam name="T">The element type of the source collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to apply to the elements.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="action"/> is null.
    /// </exception>
    public static void Apply<T>(this IEnumerable<T> source, Action<T> action)
    {
        Requires.NotNull(source);
        Requires.NotNull(action);

        foreach (var item in source)
        {
            action(item);
        }
    }

    /// <summary>
    /// Applies an action to each of the elements in the source collection and discards the
    /// results.
    /// </summary>
    /// <typeparam name="TSource">The element type of the source collection.</typeparam>
    /// <typeparam name="TResult">The result type of the action.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">The action to apply to the elements.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="action"/> is null.
    /// </exception>
    public static void Apply<TSource, TResult>(
        this IEnumerable<TSource> source,
        Func<TSource, TResult> action)
    {
        Requires.NotNull(source);
        Requires.NotNull(action);

        foreach (var item in source)
        {
            action(item);
        }
    }

    /// <summary>
    /// Applies an action to each of the elements in the source collection.
    /// </summary>
    /// <typeparam name="T">The element type of the source collection.</typeparam>
    /// <param name="source">The source collection.</param>
    /// <param name="action">
    /// The action to apply to the elements. The second parameter is the index of the element.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> or <paramref name="action"/> is null.
    /// </exception>
    public static void Apply<T>(this IEnumerable<T> source, Action<T, int> action)
    {
        Requires.NotNull(source);
        Requires.NotNull(action);

        int count = 0;
        foreach (var item in source)
        {
            action(item, count);
            count++;
        }
    }
}
