using Core.Diagnostics;

namespace Core.Linq;

/// <summary>
/// Provides extension methods for <see cref="Span{T}"/>.
/// </summary>
public static class SpanExtensions
{
    /// <summary>
    /// Sets the span of elements to a constant value.
    /// </summary>
    /// <typeparam name="T">The element type of the span.</typeparam>
    /// <param name="span">The source span to set element values.</param>
    /// <param name="value">The constant value used to set the span elements.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="span"/> is null.
    /// </exception>
    public static void SetValues<T>(this Span<T> span, T value)
    {
        for (int i = 0; i < span.Length; i++)
        {
            span[i] = value;
        }
    }

    /// <summary>
    /// Sets the span elements by applying a function to the indexes of the elements.
    /// </summary>
    /// <typeparam name="T">The element type of the span.</typeparam>
    /// <param name="span">The source span to set element values.</param>
    /// <param name="setter">
    /// The function that generates element values from element indexes.
    /// </param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="span"/> or <paramref name="setter"/> is null.
    /// </exception>
    public static void SetValues<T>(this Span<T> span, Func<int, T> setter)
    {
        Requires.NotNull(setter);

        for (int i = 0; i < span.Length; i++)
        {
            span[i] = setter(i);
        }
    }

    /// <summary>
    /// Shuffles the elements in the source span using the Fisher-Yates algorithm.
    /// </summary>
    /// <typeparam name="T">The element type of the span.</typeparam>
    /// <param name="span">The source span.</param>
    /// <param name="random">
    /// An optional <see cref="Random"/> object to generate random numbers.
    /// </param>
    public static void Shuffle<T>(this Span<T> span, Random? random = null)
    {
        random ??= new Random();
        for (int i = span.Length - 1; i >= 0; i--)
        {
            int j = random.Next(i + 1);
            (span[i], span[j]) = (span[j], span[i]);
        }
    }
}
