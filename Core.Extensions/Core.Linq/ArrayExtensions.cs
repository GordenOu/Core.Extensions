using System;
using System.Linq;
using Core.Diagnostics;

namespace Core.Linq
{
    /// <summary>
    /// Provides extension methods for <see cref="Array"/>.
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Slices the source array and copies the elements into a new array.
        /// </summary>
        /// <typeparam name="T">The element type of the array.</typeparam>
        /// <param name="source">The source array.</param>
        /// <param name="offset">The zero-based index of the first element in the slice.</param>
        /// <param name="length">The number of elements in the slice.</param>
        /// <returns>The array slice.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is null.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="offset"/> or <paramref name="length"/> is negative.
        /// </exception>
        /// <exception cref="SegmentNotInRangeException">
        /// <paramref name="offset"/> and <paramref name="length"/> are out of bound for the
        /// <paramref name="source"/> array.
        /// </exception>
        public static T[] Slice<T>(this T[] source, int offset, int length)
        {
            Requires.NotNull(source, nameof(source));
            Requires.NonNegative(offset, nameof(offset));
            Requires.NonNegative(length, nameof(length));
            Requires.SegmentInRange(source, offset, length);

            return new ArraySegment<T>(source, offset, length).ToArray();
        }
    }
}
