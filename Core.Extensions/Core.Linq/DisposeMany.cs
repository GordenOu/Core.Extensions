using Core.Diagnostics;

namespace Core.Linq;

public static partial class Enumerable
{
    /// <summary>
    /// Dispose all elements in the source collection.
    /// </summary>
    /// <param name="source">The source collection.</param>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="source"/> is null.
    /// </exception>
    public static void DisposeMany(this IEnumerable<IDisposable> source)
    {
        Requires.NotNull(source);

        foreach (var item in source)
        {
            item?.Dispose();
        }
    }
}
