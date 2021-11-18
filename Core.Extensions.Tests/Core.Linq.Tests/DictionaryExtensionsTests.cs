using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Linq.Tests;

[TestClass]
public class DictionaryExtensionsTests
{
    [TestMethod]
    public void GetOrAdd()
    {
        Dictionary<int, int>? dictionary = null;
        Assert.ThrowsException<ArgumentNullException>(() => dictionary!.GetOrAdd(1, 2));
        dictionary = new Dictionary<int, int>();

        int value = dictionary.GetOrAdd(1, 2);
        Assert.AreEqual(2, value);
        CollectionAssert.AreEquivalent(
            new[] { new KeyValuePair<int, int>(1, 2) },
            dictionary);
        value = dictionary.GetOrAdd(1, 3);
        Assert.AreEqual(2, value);
        CollectionAssert.AreEquivalent(
            new[] { new KeyValuePair<int, int>(1, 2) },
            dictionary);

        Assert.ThrowsException<ArgumentNullException>(() => dictionary.GetOrAdd(2, null!));
        value = dictionary.GetOrAdd(2, 4);
        Assert.AreEqual(4, value);
        CollectionAssert.AreEquivalent(
            new[]
            {
                    new KeyValuePair<int, int>(1, 2),
                    new KeyValuePair<int, int>(2, 4)
            },
            dictionary);
        value = dictionary.GetOrAdd(2, key => key + 1);
        Assert.AreEqual(4, value);
        CollectionAssert.AreEquivalent(
            new[]
            {
                    new KeyValuePair<int, int>(1, 2),
                    new KeyValuePair<int, int>(2, 4)
            },
            dictionary);
    }

    [TestMethod]
    public void AddOrUpdate()
    {
        Dictionary<int, int>? dictionary = null;
        Assert.ThrowsException<ArgumentNullException>(
            () => dictionary!.AddOrUpdate(1, 2, (k, v) => int.MaxValue));
        dictionary = new Dictionary<int, int>();
        Assert.ThrowsException<ArgumentNullException>(
            () => dictionary.AddOrUpdate(1, 2, null!));

        int value = dictionary.AddOrUpdate(1, 2, (k, v) => int.MaxValue);
        Assert.AreEqual(2, value);
        CollectionAssert.AreEquivalent(
            new[] { new KeyValuePair<int, int>(1, 2) },
            dictionary);
        value = dictionary.AddOrUpdate(1, 2, (k, v) => 3);
        Assert.AreEqual(3, value);
        CollectionAssert.AreEquivalent(
            new[] { new KeyValuePair<int, int>(1, 3) },
            dictionary);

        Assert.ThrowsException<ArgumentNullException>(
            () => dictionary.AddOrUpdate(2, null!, (k, v) => int.MaxValue));
        value = dictionary.AddOrUpdate(2, k => 4, (k, v) => int.MaxValue);
        Assert.AreEqual(4, value);
        CollectionAssert.AreEquivalent(
            new[]
            {
                    new KeyValuePair<int, int>(1, 3),
                    new KeyValuePair<int, int>(2, 4)
            },
            dictionary);
        value = dictionary.AddOrUpdate(2, k => 4, (k, v) => 5);
        Assert.AreEqual(5, value);
        CollectionAssert.AreEquivalent(
            new[]
            {
                    new KeyValuePair<int, int>(1, 3),
                    new KeyValuePair<int, int>(2, 5)
            },
            dictionary);
    }
}
