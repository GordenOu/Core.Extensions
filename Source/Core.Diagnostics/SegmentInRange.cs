using System;
using System.Collections.Generic;
using System.Diagnostics;
using Core.Diagnostics.Resources;

namespace Core
{
    /// <summary>
    /// The exception that is thrown when there is an attempt to create a segment of invalid range
    /// out of a collection.
    /// </summary>
    public class SegmentNotInRangeException : ArgumentException
    {
        /// <summary>
        /// Gets the number of elements in the source collection.
        /// </summary>
        public int Range { get; }

        /// <summary>
        /// The zero-based index of the first element in the segment.
        /// </summary>
        public int Offset { get; }

        /// <summary>
        /// Gets the number of elements in the segment.
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SegmentNotInRangeException"/> class with
        /// the range of the source collection and the parameters used to construct the segment.
        /// </summary>
        /// <param name="range">The number of elements in the collection.</param>
        /// <param name="offset">The zero-based index of the first element in the segment.</param>
        /// <param name="count">The number of elements in the segment.</param>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        public SegmentNotInRangeException(int range, int offset, int count, string message)
            : base(message)
        {
            Range = range;
            Offset = offset;
            Count = count;
        }
    }
}

namespace Core.Diagnostics
{
    public partial class Requires
    {
        /// <summary>
        /// Throws <see cref="SegmentNotInRangeException"/> if <paramref name="offset"/> and
        /// <paramref name="count"/> are out of bound for the <paramref name="source"/> collection.
        /// </summary>
        /// <typeparam name="T">The element type of the collection.</typeparam>
        /// <param name="source">The source collection.</param>
        /// <param name="offset">The zero-based index of the first element in the segment.</param>
        /// <param name="count">The number of elements in the segment.</param>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="source"/> is empty.
        /// </exception>
        /// <exception cref="ArgumentOutOfRangeException">
        /// <paramref name="count"/> is negative.
        /// </exception>
        /// <exception cref="SegmentNotInRangeException">
        /// <paramref name="offset"/> and <paramref name="count"/> are out of bound for the
        /// <paramref name="source"/> collection.
        /// </exception>
        [DebuggerStepThrough]
        public static void SegmentInRange<T>(ICollection<T> source, int offset, int count)
        {
            if (source is null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count), count, Strings.NonNegative);
            }

            if (!(offset >= 0 && offset + count <= source.Count))
            {
                throw new SegmentNotInRangeException(source.Count, offset, count, string.Empty);
            }
        }
    }
}
