using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Reflection.Tests;

[TestClass]
public class TypeExtensionsTests
{
    [TestMethod]
    public void GetDefaultValueOfClassAndStruct()
    {
        Assert.ThrowsException<ArgumentNullException>(() => ((Type)null!).GetDefaultValue());

        Assert.IsNull(typeof(TypeExtensionsTests).GetDefaultValue());
        Assert.AreEqual(default(DateTime), typeof(DateTime).GetDefaultValue());
    }
}
