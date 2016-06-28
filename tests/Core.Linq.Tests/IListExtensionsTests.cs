using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Linq.Enumerable;

namespace Core.Linq.Tests
{
    [TestClass]
    public class IListExtensionsTests
    {
        [TestMethod]
        public void SetValues()
        {
            int[] array = null;
            Assert.ThrowsException<ArgumentNullException>(() => array.SetValues(1));
            Assert.ThrowsException<ArgumentNullException>(() => array.SetValues(i => i + 2));
            array = new int[5];

            array.SetValues(2);
            CollectionAssert.AreEqual(new[] { 2, 2, 2, 2, 2 }, array);

            Assert.ThrowsException<ArgumentNullException>(() => array.SetValues(null));
            array.SetValues(i => i + 2);
            CollectionAssert.AreEqual(new[] { 2, 3, 4, 5, 6 }, array);
        }

        [TestMethod]
        public void Shuffle()
        {
            int[] array = null;
            Assert.ThrowsException<ArgumentNullException>(() => array.Shuffle());

            var array1 = Range(0, 100).ToArray();
            var array2 = array1.ToArray();
            array2.Shuffle();
            CollectionAssert.AreNotEqual(array1, array2);
            CollectionAssert.AreEquivalent(array1, array2);
            array2.Shuffle(new Random());
            CollectionAssert.AreNotEqual(array1, array2);
            CollectionAssert.AreEquivalent(array1, array2);
        }
    }
}
