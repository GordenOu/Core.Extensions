using System.Globalization;
using System.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Resources.Tests;

[TestClass]
public class StringResourceTests : StringResource<StringResourceTests>
{
    [TestMethod]
    public void GetExistingResource()
    {
        var culture = CultureInfo.GetCultureInfo("en");
        Assert.AreEqual("Hello", GetString("Test", culture));

        CultureInfo.CurrentUICulture = culture;
        Assert.AreEqual("Hello", GetString("Test"));

        culture = CultureInfo.GetCultureInfo("en-US");
        Assert.AreEqual("Hello", GetString("Test", culture));

        CultureInfo.CurrentUICulture = culture;
        Assert.AreEqual("Hello", GetString("Test"));
    }

    [TestMethod]
    public void GetNonexistentResource()
    {
        var culture1 = CultureInfo.GetCultureInfo("en");
        var culture2 = CultureInfo.GetCultureInfo("zh");
        Assert.ThrowsException<MissingManifestResourceException>(() => GetString("test", culture1));
        Assert.ThrowsException<MissingManifestResourceException>(() => GetString("test", culture2));

        CultureInfo.CurrentUICulture = culture1;
        Assert.ThrowsException<MissingManifestResourceException>(() => GetString("test"));
        CultureInfo.CurrentUICulture = culture2;
        Assert.ThrowsException<MissingManifestResourceException>(() => GetString("test"));
    }
}
