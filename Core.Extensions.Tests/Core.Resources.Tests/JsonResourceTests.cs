using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text.Json;
using Core.Resources.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Resources.Tests
{
    [TestClass]
    public class JsonResourceTests : JsonResource<JsonResourceTests>
    {
        [TestMethod]
        public void GetExistingResource()
        {
            var culture = CultureInfo.GetCultureInfo("zh-Hans");
            Assert.AreEqual("测试", GetString("Test", culture));

            CultureInfo.CurrentUICulture = culture;
            Assert.AreEqual("测试", GetString("Test"));

            var element = GetJsonElement("bool");
            Assert.AreEqual(JsonValueKind.True, element?.ValueKind);

            element = GetJsonElement("object");
            Assert.AreEqual(JsonValueKind.Object, element?.ValueKind);
            var properties = element?.EnumerateObject().ToArray();
            Assert.AreEqual(1, properties.Length);
            Assert.AreEqual("name", properties[0].Name);
            Assert.AreEqual(JsonValueKind.String, properties[0].Value.ValueKind);
            Assert.AreEqual("value", properties[0].Value.GetString());

            element = GetJsonElement("array");
            Assert.AreEqual(JsonValueKind.Array, element?.ValueKind);
            var elements = element?.EnumerateArray().ToArray();
            Assert.AreEqual(3, elements.Length);
            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(JsonValueKind.String, elements[i].ValueKind);
                Assert.AreEqual($"test{i + 1}", elements[i].GetString());
            }
        }

        [TestMethod]
        public void GetNonexistentResource()
        {
            var culture = CultureInfo.GetCultureInfo("zh-Hans");
            Assert.ThrowsException<MissingManifestResourceException>(() => GetString("test", culture));

            CultureInfo.CurrentUICulture = culture;
            Assert.ThrowsException<MissingManifestResourceException>(() => GetString("test"));
        }
    }
}
