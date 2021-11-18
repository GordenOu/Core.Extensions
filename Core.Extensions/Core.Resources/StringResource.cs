using System.Globalization;
using System.Resources;

namespace Core.Resources;

/// <summary>
/// When inherited, the derived class can retrieve localized string resource through
/// <see cref="ResourceManager"/> by calling the <see cref="GetString(string)"/> method.
/// </summary>
/// <typeparam name="T">The strongly typed class representing the resource.</typeparam>
public abstract class StringResource<T>
    where T : StringResource<T>
{
    private static readonly ResourceManager resourceManager = new(typeof(T));

    /// <summary>
    /// Initializes a new instance of the <see cref="StringResource{T}"/> class.
    /// </summary>
    protected StringResource() { }

    /// <summary>
    /// Gets the localized string resource of the given name.
    /// </summary>
    /// <param name="name">The name of the string resource.</param>
    /// <returns>The string resource, or null if the resource is not found.</returns>
    /// <exception cref="ArgumentNullException">
    /// <paramref name="name"/> is null.
    /// </exception>
    public static string? GetString(string name)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return resourceManager.GetString(name);
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
    public static string? GetString(string name, CultureInfo culture)
    {
        if (name is null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        if (culture is null)
        {
            throw new ArgumentNullException(nameof(culture));
        }

        return resourceManager.GetString(name, culture);
    }
}
