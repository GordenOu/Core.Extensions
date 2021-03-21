using System;
using System.Collections.Generic;
using Core.Diagnostics;

namespace Core.Linq
{
    /// <summary>
    /// Provides extension methods for classes that implement <see cref="IList{T}"/>.
    /// </summary>
    public static class IListExtensions
    {
        /// <summary>
        /// Sets the list elements to a constant value.
        /// </summary>
        /// <typeparam name="T">The element type of the list.</typeparam>
        /// <param name="list">The source list to set element values.</param>
        /// <param name="value">The constant value used to set the list elements.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list"/> is null.
        /// </exception>
        public static void SetValues<T>(this IList<T> list, T value)
        {
            Requires.NotNull(list, nameof(list));

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = value;
            }
        }

        /// <summary>
        /// Sets the list elements by applying a function to the indexes of the elements.
        /// </summary>
        /// <typeparam name="T">The element type of the list.</typeparam>
        /// <param name="list">The source list to set element values.</param>
        /// <param name="setter">
        /// The function that generates element values from element indexes.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="list"/> or <paramref name="setter"/> is null.
        /// </exception>
        public static void SetValues<T>(this IList<T> list, Func<int, T> setter)
        {
            Requires.NotNull(list, nameof(list));
            Requires.NotNull(setter, nameof(setter));

            for (int i = 0; i < list.Count; i++)
            {
                list[i] = setter(i);
            }
        }

        /// <summary>
        /// Shuffles the elements in the source list using the Fisher-Yates algorithm.
        /// </summary>
        /// <typeparam name="T">The element type of the list.</typeparam>
        /// <param name="list">The source list.</param>
        /// <param name="random">
        /// An optional <see cref="Random"/> object to generate random numbers.
        /// </param>
        public static void Shuffle<T>(this IList<T> list, Random random = null)
        {
            Requires.NotNull(list, nameof(list));

            random ??= new Random();
            for (int i = list.Count - 1; i >= 0; i--)
            {
                int j = random.Next(i + 1);
                (list[i], list[j]) = (list[j], list[i]);
            }
        }
    }
}
