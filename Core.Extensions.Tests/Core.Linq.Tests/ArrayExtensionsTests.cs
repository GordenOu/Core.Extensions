using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Linq.Tests
{
    [TestClass]
    public class ArrayExtensionsTests
    {
        [TestMethod]
        public void Preconditions()
        {
            int[] array = null;
            Assert.ThrowsException<ArgumentNullException>(() => array.Slice(0, 0));
            array = new int[5];
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.Slice(-1, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => array.Slice(0, -1));
            Assert.ThrowsException<SegmentNotInRangeException>(() => array.Slice(1, 5));
        }

        [TestMethod]
        public void Slice()
        {
            var array = new[] { 1, 2, 3 };
            var slice = array.Slice(1, 0);
            Assert.AreEqual(0, slice.Length);
            slice = array.Slice(0, 3);
            CollectionAssert.AreEqual(array, slice);
            slice = array.Slice(1, 2);
            Assert.AreEqual(2, slice.Length);
            Assert.AreEqual(2, slice[0]);
            Assert.AreEqual(3, slice[1]);
        }
    }
}
