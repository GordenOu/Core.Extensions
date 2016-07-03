using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Runtime.Serialization.Tests
{
    [DataContract]
    public class Class1<T>
    {
        [DataMember]
        public ReadOnlyCollection<T> List { get; set; }
    }

    [DataContract]
    public class Class2<T> : Class1<T>
    {
        [DataMember]
        public Class1<T> Value { get; set; }
    }

    [TestClass]
    public class AnyTypeResolverTests
    {
        [TestMethod]
        public void Serialization()
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

            var serializer = new DataContractSerializer(
                typeof(object),
                new DataContractSerializerSettings()
                {
                    DataContractResolver = new AnyTypeResolver(
                        new[] { typeof(Class1<int>).GetTypeInfo().Assembly }),
                    SerializeReadOnlyTypes = true
                });
            var output = new StringBuilder();
            using (var writer = XmlWriter.Create(output))
            {
                serializer.WriteObject(writer, obj1);
                writer.Flush();
            }

            string xml = output.ToString();
            Class2<int> obj2;
            using (var reader = XmlReader.Create(new StringReader(xml)))
            {
                obj2 = serializer.ReadObject(reader) as Class2<int>;
            }

            Assert.IsNotNull(obj2);
            CollectionAssert.AreEqual(((Class2<int>)obj2.Value).Value.List, new[] { 1, 2, 3 });
            CollectionAssert.AreEqual(obj2.Value.List, new[] { 1, 3, 2 });
            CollectionAssert.AreEqual(obj2.List, new[] { 3, 2, 1 });
        }
    }
}
