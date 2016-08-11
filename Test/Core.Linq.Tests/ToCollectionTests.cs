using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Linq.Tests
{
    [TestClass]
    public class ToCollectionTests
    {
        [TestMethod]
        public void ToArray()
        {
            int[] array = null;
            Func<int, int> selector1 = null;
            Func<int, int, int> selector2 = null;
            Assert.ThrowsException<ArgumentNullException>(() => array.ToArray(i => i * 2));
            Assert.ThrowsException<ArgumentNullException>(() => array.ToArray((i, j) => i * j));

            array = new[] { 1, 2, 3 }.ToArray();
            Assert.ThrowsException<ArgumentNullException>(() => array.ToArray(selector1));
            Assert.ThrowsException<ArgumentNullException>(() => array.ToArray(selector2));

            selector1 = i => i * 2;
            selector2 = (i, j) => i * j;

            var newArray = array.ToArray(selector1);
            Assert.AreNotSame(array, newArray);
            CollectionAssert.AreEqual(new[] { 2, 4, 6 }, newArray);

            newArray = array.ToArray(selector2);
            Assert.AreNotSame(array, newArray);
            CollectionAssert.AreEqual(new[] { 0, 2, 6 }, newArray);
        }

        [TestMethod]
        public void ToList()
        {
            List<int> list = null;
            Func<int, int> selector1 = null;
            Func<int, int, int> selector2 = null;
            Assert.ThrowsException<ArgumentNullException>(() => list.ToList(i => i * 2));
            Assert.ThrowsException<ArgumentNullException>(() => list.ToList((i, j) => i * j));

            list = new[] { 1, 2, 3 }.ToList();
            Assert.ThrowsException<ArgumentNullException>(() => list.ToList(selector1));
            Assert.ThrowsException<ArgumentNullException>(() => list.ToList(selector2));

            selector1 = i => i * 2;
            selector2 = (i, j) => i * j;

            var newList = list.ToList(selector1);
            Assert.AreNotSame(list, newList);
            CollectionAssert.AreEqual(new[] { 2, 4, 6 }, newList);

            newList = list.ToList(selector2);
            Assert.AreNotSame(list, newList);
            CollectionAssert.AreEqual(new[] { 0, 2, 6 }, newList);
        }

        [TestMethod]
        public void ToReadOnlyCollection()
        {
            int[] array = null;
            Assert.ThrowsException<ArgumentNullException>(() => array.ToReadOnlyCollection());
            array = new[] { 1, 2, 3 };
            CollectionAssert.AreEqual(array.ToReadOnlyCollection(), array);
        }
    }
}
