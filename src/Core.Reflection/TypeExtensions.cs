using System;
using System.Reflection;
using Core.Diagnostics;

namespace Core.Reflection
{
    /// <summary>
    /// Provides methods that retrieve information about types at run time.
    /// </summary>
    public static class TypeExtensions
    {
        /// <summary>
        /// Gets a default value of the specified type.
        /// </summary>
        /// <param name="type">The specified type.</param>
        /// <returns>The default value of the specified type.</returns>
        public static object GetDefaultValue(this Type type)
        {
            Requires.NotNull(type, nameof(type));

            return type.GetTypeInfo().IsValueType ? Activator.CreateInstance(type) : null;
        }
    }
}
