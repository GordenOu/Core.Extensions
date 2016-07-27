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
            Assert.AreEqual("Hello", GetString("Test"));
        }

        [TestMethod]
        public void GetNonexistentResource()
        {
            Assert.ThrowsException<MissingManifestResourceException>(() => GetString("test"));
        }
    }
}
