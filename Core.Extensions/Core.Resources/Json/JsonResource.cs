using System;
using System.Globalization;
using System.Text.Json;

namespace Core.Resources.Json
{
    /// <summary>
    /// When inherited, the derived class can retrieve localized resources by calling
    /// the <see cref="GetString(string)"/> or <see cref="GetJsonElement(string)"/> method.
    /// </summary>
    /// <typeparam name="T">The strongly typed class representing the resource.</typeparam>
    public class JsonResource<T>
    {
        private static readonly JsonResourceManager resourceManager = new JsonResourceManager(typeof(T));

        /// <summary>
        /// Initializes a new instance of the <see cref="StringResource{T}"/> class.
        /// </summary>
        protected JsonResource() { }

        /// <summary>
        /// Gets the localized string resource of the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns>The string resource, or null if the resource is not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The resource is not of a string.
        /// </exception>
        public static string GetString(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var value = resourceManager.GetObject(name);
            if (value != null)
            {
                var element = (JsonElement)value;
                if (element.ValueKind == JsonValueKind.String)
                {
                    return element.GetString();
                }
                else
                {
                    throw new InvalidOperationException(nameof(JsonValueKind));
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the localized string resource of the given name and culture.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="culture">The culture of the string resource.</param>
        /// <returns>The string resource, or null if the resource is not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="culture"/> is null.
        /// </exception>
        /// <exception cref="InvalidOperationException">
        /// The resource is not of a string.
        /// </exception>
        public static string GetString(string name, CultureInfo culture)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (culture is null)
            {
                throw new ArgumentNullException(nameof(culture));
            }

            var value = resourceManager.GetObject(name, culture);
            if (value != null)
            {
                var element = (JsonElement)value;
                if (element.ValueKind == JsonValueKind.String)
                {
                    return element.GetString();
                }
                else
                {
                    throw new InvalidOperationException(nameof(JsonValueKind));
                }
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the localized <see cref="JsonElement"/> resource of the given name.
        /// </summary>
        /// <param name="name">The name of the <see cref="JsonElement"/> resource.</param>
        /// <returns>The <see cref="JsonElement"/> resource, or null if the resource is not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> is null.
        /// </exception>
        public static JsonElement? GetJsonElement(string name)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var value = resourceManager.GetObject(name);
            if (value != null)
            {
                return (JsonElement)value;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the localized <see cref="JsonElement"/> resource of the given name and culture.
        /// </summary>
        /// <param name="name">The name of the <see cref="JsonElement"/> resource.</param>
        /// <param name="culture">The culture of the <see cref="JsonElement"/> resource.</param>
        /// <returns>The <see cref="JsonElement"/> resource, or null if the resource is not found.</returns>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="name"/> is null.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// <paramref name="culture"/> is null.
        /// </exception>
        public static JsonElement? GetJsonElement(string name, CultureInfo culture)
        {
            if (name is null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            var value = resourceManager.GetObject(name, culture);
            if (value != null)
            {
                return (JsonElement)value;
            }
            else
            {
                return null;
            }
        }
    }
}
