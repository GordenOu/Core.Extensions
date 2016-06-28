﻿using System;
using System.Collections.Generic;
using Core.Diagnostics;

namespace Core.Linq
{
    /// <summary>
    /// Provides extension methods for <see cref="Dictionary{TKey, TValue}"/>.
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds a key-value pair to the dictionary if the key does not exist.
        /// </summary>
        /// <typeparam name="TKey">The key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">The value type of the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="value">The value of the key-value pair.</param>
        /// <returns>
        /// The corresponding value in the dictionary if <paramref name="key"/> already exists, 
        /// or <paramref name="value"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> is null.
        /// </exception>
        public static TValue GetOrAdd<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            TValue value)
        {
            Requires.NotNull(dictionary, nameof(dictionary));

            TValue temp;
            if (dictionary.TryGetValue(key, out temp))
            {
                return temp;
            }
            else
            {
                dictionary.Add(key, value);
                return value;
            }
        }

        /// <summary>
        /// Generates a value and adds the key-value pair to the dictionary if the key does not
        /// exist.
        /// </summary>
        /// <typeparam name="TKey">The key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">The value type of the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="valueFactory">
        /// The function used to generate the value from the key.
        /// </param>
        /// <returns>
        /// The corresponding value in the dictionary if <paramref name="key"/> already exists, 
        /// or the value generated by <paramref name="valueFactory"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> or <paramref name="valueFactory"/> is null.
        /// </exception>
        public static TValue GetOrAdd<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TKey, TValue> valueFactory)
        {
            Requires.NotNull(dictionary, nameof(dictionary));
            Requires.NotNull(valueFactory, nameof(valueFactory));

            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                return value;
            }
            else
            {
                value = valueFactory(key);
                dictionary.Add(key, value);
                return value;
            }
        }

        /// <summary>
        /// Adds a key-value pair to the dictionary if the key does not exist, or updates the value
        /// of the key using the given function.
        /// </summary>
        /// <typeparam name="TKey">The key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">The value type of the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="addValue">
        /// The value to be added to the dictionary if the key does not exists.
        /// </param>
        /// <param name="updateValueFactory">
        /// The function used to generate the updated value from the existing key and value.
        /// </param>
        /// <returns>
        /// <paramref name="addValue"/> if <paramref name="key"/> does not exists, or the value
        /// generated by <paramref name="updateValueFactory"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> or <paramref name="updateValueFactory"/> is null.
        /// </exception>
        public static TValue AddOrUpdate<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            TValue addValue,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            Requires.NotNull(dictionary, nameof(dictionary));
            Requires.NotNull(updateValueFactory, nameof(updateValueFactory));

            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                value = updateValueFactory(key, value);
                dictionary[key] = value;
                return value;
            }
            else
            {
                dictionary.Add(key, addValue);
                return addValue;
            }
        }

        /// <summary>
        /// Generates a value and adds the key-value pair to the dictionary if the key does not
        /// exist, or updates the value of the key using the given function.
        /// </summary>
        /// <typeparam name="TKey">The key type of the dictionary.</typeparam>
        /// <typeparam name="TValue">The value type of the dictionary.</typeparam>
        /// <param name="dictionary">The source dictionary.</param>
        /// <param name="key">The key of the key-value pair.</param>
        /// <param name="addValueFactory">
        /// The function used to generate the new value from the key.
        /// </param>
        /// <param name="updateValueFactory">
        /// The function used to generate the updated value from the existing key and value.
        /// </param>
        /// <returns>
        /// The value generated by <paramref name="addValueFactory"/> if <paramref name="key"/>
        /// does not exists, or the value generated by <paramref name="updateValueFactory"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="dictionary"/> or <paramref name="addValueFactory"/> or
        /// <paramref name="updateValueFactory"/> is null.
        /// </exception>
        public static TValue AddOrUpdate<TKey, TValue>(
            this Dictionary<TKey, TValue> dictionary,
            TKey key,
            Func<TKey, TValue> addValueFactory,
            Func<TKey, TValue, TValue> updateValueFactory)
        {
            Requires.NotNull(dictionary, nameof(dictionary));
            Requires.NotNull(addValueFactory, nameof(addValueFactory));
            Requires.NotNull(updateValueFactory, nameof(updateValueFactory));

            TValue value;
            if (dictionary.TryGetValue(key, out value))
            {
                value = updateValueFactory(key, value);
                dictionary[key] = value;
                return value;
            }
            else
            {
                value = addValueFactory(key);
                dictionary.Add(key, value);
                return value;
            }
        }
    }
}
