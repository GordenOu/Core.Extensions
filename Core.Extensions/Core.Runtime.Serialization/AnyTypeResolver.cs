﻿using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Core.Runtime.Serialization;

/// <summary>
/// Tries to resolve any type mapping using type names and assembly names.
/// </summary>
public class AnyTypeResolver : DataContractResolver
{
    private Type? cachedType;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnyTypeResolver"/> class.
    /// </summary>
    public AnyTypeResolver() { }

    /// <summary>
    /// Resolve any type so that the full type name maps to the XML type name and the assembly
    /// name maps to the XML namespace.
    /// </summary>
    /// <param name="type">The underlying type of the object.</param>
    /// <param name="declaredType">The type declared in the data contract.</param>
    /// <param name="knownTypeResolver">This parameter is ignored.</param>
    /// <param name="typeName">The mapped XML type name.</param>
    /// <param name="typeNamespace">The mapped XML namespace.</param>
    /// <returns>True unless exceptions occurred.</returns>
    public override bool TryResolveType(
        Type type,
        Type? declaredType,
        DataContractResolver knownTypeResolver,
        out XmlDictionaryString? typeName,
        out XmlDictionaryString? typeNamespace)
    {
        if (type is null || declaredType is null)
        {
            typeName = null;
            typeNamespace = null;
            return false;
        }
        else
        {
            string? fullName = type.FullName;
            string assemblyName = type.GetTypeInfo().Assembly.GetName().FullName;
            var dictionary = new XmlDictionary();
            typeName = dictionary.Add(XmlConvert.EncodeName(fullName) ?? string.Empty);
            typeNamespace = dictionary.Add(XmlConvert.EncodeName(assemblyName));
            cachedType = type;
            return true;
        }
    }

    /// <summary>
    /// Maps a XML type to a CLR type.
    /// </summary>
    /// <param name="typeName">The XML type name.</param>
    /// <param name="typeNamespace">The XML namespace.</param>
    /// <param name="declaredType">The type declared in the data contract.</param>
    /// <param name="knownTypeResolver">This parameter is ignored.</param>
    /// <returns>The corresponding CLR type, or null if no such type can be found.</returns>
    public override Type? ResolveName(
        string typeName,
        string? typeNamespace,
        Type? declaredType,
        DataContractResolver knownTypeResolver)
    {
        typeName ??= string.Empty;
        typeNamespace ??= string.Empty;
        typeName = XmlConvert.DecodeName(typeName);
        typeNamespace = XmlConvert.DecodeName(typeNamespace);

        var type = Type.GetType(string.Join(",", typeName, typeNamespace));

        // Bug
        if (type is null && typeNamespace.StartsWith("http://"))
        {
            type = cachedType;
            cachedType = null;
        }

        return type;
    }
}
