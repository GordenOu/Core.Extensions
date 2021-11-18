using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Linq.Enumerable;

namespace Core.Linq.Tests;

[TestClass]
public class SpanExtensionsTests
{
    [TestMethod]
    public void SetValues()
    {
        var array = new int[5];

        array.AsSpan().SetValues(2);
        CollectionAssert.AreEqual(new[] { 2, 2, 2, 2, 2 }, array);

        Assert.ThrowsException<ArgumentNullException>(() => array.AsSpan().SetValues(null!));
        array.AsSpan().SetValues(i => i + 2);
        CollectionAssert.AreEqual(new[] { 2, 3, 4, 5, 6 }, array);
    }

    [TestMethod]
    public void Shuffle()
    {
        var array1 = Range(0, 100).ToArray();
        var array2 = array1.ToArray();
        array2.AsSpan().Shuffle();
        CollectionAssert.AreNotEqual(array1, array2);
        CollectionAssert.AreEquivalent(array1, array2);
        array2.AsSpan().Shuffle(new Random());
        CollectionAssert.AreNotEqual(array1, array2);
        CollectionAssert.AreEquivalent(array1, array2);
    }
}
