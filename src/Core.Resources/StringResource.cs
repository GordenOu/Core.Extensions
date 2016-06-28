using System;
using System.Resources;

namespace Core.Resources
{
    /// <summary>
    /// When inherited, the derived class can retrieve localized string resource through
    /// <see cref="ResourceManager"/> by calling the <see cref="GetString(string)"/> method.
    /// </summary>
    /// <typeparam name="T">The strongly typed class representing the resource.</typeparam>
    public abstract class StringResource<T>
        where T : StringResource<T>
    {
        private static ResourceManager resourceManager = new ResourceManager(typeof(T));

        protected StringResource() { }

        /// <summary>
        /// Gets the localized string resource of the given name.
        /// </summary>
        /// <param name="name">The name of the string resource.</param>
        /// <returns>The string resource.</returns>
        public static string GetString(string name)
        {
            if (name == null)
            {
                throw new ArgumentNullException(nameof(name));
            }

            return resourceManager.GetString(name);
        }
    }
}
