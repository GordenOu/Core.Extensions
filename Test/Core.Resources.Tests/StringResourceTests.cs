using System.Globalization;
using System.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Resources.Tests
{
    [TestClass]
    public class StringResourceTests : StringResource<StringResourceTests>
    {
        [TestMethod]
        public void GetExistingResource()
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            Assert.AreEqual("Hello", GetString("Test"));
        }

        [TestMethod]
        public void GetNonexistentResource()
        {
            CultureInfo.CurrentUICulture = CultureInfo.GetCultureInfo("en");
            Assert.ThrowsException<MissingManifestResourceException>(() => GetString("test"));
        }
    }
}
