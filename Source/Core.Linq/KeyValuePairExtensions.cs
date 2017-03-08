using System.Collections.Generic;

namespace Core.Linq
{
    /// <summary>
    /// Provides extension methods for <see cref="KeyValuePair{TKey, TValue}"/>.
    /// </summary>
    public static class KeyValuePairExtensions
    {
        /// <summary>
        /// Deconstructs a <see cref="KeyValuePair{TKey, TValue}"/> into its key and value.
        /// </summary>
        /// <typeparam name="TKey">The key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">The value type of the dictionary.</typeparam>
        /// <param name="pair">The key-value pair to be deconstructed.</param>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="value">The value of the key-value pair.</param>
        public static void Deconstruct<TKey, TValue>(
            this KeyValuePair<TKey, TValue> pair,
            out TKey key,
            out TValue value)
        {
            (key, value) = (pair.Key, pair.Value);
        }
    }
}
