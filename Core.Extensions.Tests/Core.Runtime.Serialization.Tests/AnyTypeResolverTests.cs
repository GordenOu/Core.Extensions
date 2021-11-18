using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Core.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Runtime.Serialization.Tests;

[DataContract]
public class Class1<T>
{
    [DataMember]
    public ReadOnlyCollection<T>? List { get; set; }
}

[DataContract]
public class Class2<T> : Class1<T>
{
    [DataMember]
    public Class1<T>? Value { get; set; }
}

[TestClass]
public class AnyTypeResolverTests
{
    private static string Serialize<T>(T obj)
    {
        var serializer = new DataContractSerializer(
            typeof(T),
            new DataContractSerializerSettings()
            {
                DataContractResolver = new AnyTypeResolver(),
                SerializeReadOnlyTypes = true
            });
        var output = new StringBuilder();
        using (var writer = XmlWriter.Create(output))
        {
            serializer.WriteObject(writer, obj);
            writer.Flush();
        }
        return output.ToString();
    }

    private static T? Deserialize<T>(string xml)
    {
        var serializer = new DataContractSerializer(
            typeof(T),
            new DataContractSerializerSettings()
            {
                DataContractResolver = new AnyTypeResolver(),
                SerializeReadOnlyTypes = true
            });
        using var reader = XmlReader.Create(new StringReader(xml));
        return (T?)serializer.ReadObject(reader);
    }

    [TestMethod]
    public void SerializeBasicTypes()
    {
        int a = 3;
        string xml = Serialize<object>(a);
        int b = Deserialize<int>(xml);
        Assert.AreEqual(a, b);

        var list = new List<int> { 1, 2, 3 };
        xml = Serialize(list);
        var array = Deserialize<int[]>(xml);
        CollectionAssert.AreEqual(list, array);

        xml = Serialize<IList<int>>(list);
        var newList = Deserialize<List<int>>(xml);
        CollectionAssert.AreEqual(list, newList);
    }

    [TestMethod]
    public void SerializeComplexTypes()
    {
        Class1<int> obj1 = new Class2<int>
        {
            Value = new Class2<int>
            {
                Value = new Class1<int>
                {
                    List = new[]
                    {
                            1,
                            2,
                            3
                        }.ToList().AsReadOnly()
                },
                List = new[]
                {
                        1,
                        3,
                        2
                    }.ToList().AsReadOnly()
            },
            List = new[]
            {
                    3,
                    2,
                    1
                }.ToList().AsReadOnly()
        };

        string xml = Serialize<object>(obj1);
        var obj2 = Deserialize<Class2<int>>(xml);

        Assert.IsNotNull(obj2);
        CollectionAssert.AreEqual(((Class2<int>?)obj2?.Value)?.Value?.List, new[] { 1, 2, 3 });
        CollectionAssert.AreEqual(obj2?.Value?.List, new[] { 1, 3, 2 });
        CollectionAssert.AreEqual(obj2?.List, new[] { 3, 2, 1 });
    }

    [TestMethod]
    public void SerializeClassesInDifferentAssemblies()
    {
        Class3<int> obj1 = new Class4<int>
        {
            Value = new Class4<int>
            {
                Value = new Class3<int>
                {
                    List = new[]
                    {
                            1,
                            2,
                            3
                        }.ToList().AsReadOnly()
                },
                List = new[]
                {
                        1,
                        3,
                        2
                    }.ToList().AsReadOnly()
            },
            List = new[]
            {
                    3,
                    2,
                    1
                }.ToList().AsReadOnly()
        };

        string xml = Serialize(obj1);
        var obj2 = Deserialize<Class4<int>>(xml);

        Assert.IsNotNull(obj2);
        CollectionAssert.AreEqual(((Class4<int>)obj2.Value).Value.List, new[] { 1, 2, 3 });
        CollectionAssert.AreEqual(obj2.Value.List, new[] { 1, 3, 2 });
        CollectionAssert.AreEqual(obj2.List, new[] { 3, 2, 1 });
    }
}
