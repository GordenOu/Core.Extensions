using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Core.Diagnostics;

namespace Core.Linq
{
    public static partial class Enumerable
    {
        /// <summary>
        /// Creates a new array whose elements are mapped from the source array.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TResult">The element type of the result array.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="selector">The function used to map the source array elements.</param>
        /// <returns>
        /// A new array whose elements are mapped from the source array using the
        /// <paramref name="selector"/> function.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static TResult[] ToArray<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(selector, nameof(selector));

            return source.Select(selector).ToArray();
        }

        /// <summary>
        /// Creates a new array whose elements are mapped from the source array elements and
        /// indexes.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source array.</typeparam>
        /// <typeparam name="TResult">The element type of the result array.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="selector">
        /// The function used to map the source array elements. The second parameter is the index
        /// of the element.
        /// </param>
        /// <returns>
        /// A new array whose elements are mapped from the source array using the
        /// <paramref name="selector"/> function.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static TResult[] ToArray<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, TResult> selector)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(selector, nameof(selector));

            return source.Select(selector).ToArray();
        }

        /// <summary>
        /// Creates a new list whose elements are mapped from the source list.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TResult">The element type of the result list.</typeparam>
        /// <param name="source">The source list.</param>
        /// <param name="selector">The function used to map the source list elements.</param>
        /// <returns>
        /// A new list whose elements are mapped from the source list using the
        /// <paramref name="selector"/> function.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static List<TResult> ToList<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, TResult> selector)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(selector, nameof(selector));

            return source.Select(selector).ToList();
        }

        /// <summary>
        /// Creates a new list whose elements are mapped from the source list elements and
        /// indexes.
        /// </summary>
        /// <typeparam name="TSource">The element type of the source list.</typeparam>
        /// <typeparam name="TResult">The element type of the result list.</typeparam>
        /// <param name="source">The source list.</param>
        /// <param name="selector">
        /// The function used to map the source list elements. The second parameter is the index
        /// of the element.
        /// </param>
        /// <returns>
        /// A new list whose elements are mapped from the source list using the
        /// <paramref name="selector"/> function.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> or <paramref name="selector"/> is null.
        /// </exception>
        public static List<TResult> ToList<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, TResult> selector)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NotNull(selector, nameof(selector));

            return source.Select(selector).ToList();
        }

        /// <summary>
        /// Creates a <see cref="ReadOnlyCollection{T}"/> from an <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T">The element type of the source collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <returns>
        /// A <see cref="ReadOnlyCollection{T}"/> that contains elements from the input sequence.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        public static ReadOnlyCollection<T> ToReadOnlyCollection<T>(this IEnumerable<T> source)
        {
            Requires.NotNull(source, nameof(source));

            return source.ToList().AsReadOnly();
        }
    }
}
