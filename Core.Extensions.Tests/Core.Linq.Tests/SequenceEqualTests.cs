using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Linq.Tests
{
    [TestClass]
    public class SequenceEqualTests
    {
        [TestMethod]
        public void ParamsArray()
        {
            int[] first = null;
            int[] second = null;
            Assert.ThrowsException<ArgumentNullException>(() => first.SequenceEqual(1, 2, 3));
            first = new[] { 1, 2, 3 };
            Assert.ThrowsException<ArgumentNullException>(() => first.SequenceEqual(second));

            second = new[] { 1, 2, 3 };
            Assert.IsTrue(first.SequenceEqual(second));
            Assert.IsTrue(first.SequenceEqual(1, 2, 3));
            Assert.IsFalse(first.SequenceEqual(4, 5, 6));
        }

        [TestMethod]
        public void UseEqualityComparison()
        {
            int[] first = null;
            int[] second = null;
            static bool equalityComparison(int x, int y) => x == y;
            Assert.ThrowsException<ArgumentNullException>(
                () => first.SequenceEqual(new[] { 1, 2, 3 }, equalityComparison));
            first = new[] { 1, 2, 3 };
            Assert.ThrowsException<ArgumentNullException>(
                () => first.SequenceEqual(second, equalityComparison));

            Assert.IsTrue(first.SequenceEqual(new[] { 1, 2, 3 }, null));
            Assert.IsTrue(first.SequenceEqual(new[] { 1, 2, 3 }, equalityComparison));
            Assert.IsFalse(first.SequenceEqual(new[] { 4, 5, 6 }, null));
            Assert.IsTrue(first.SequenceEqual(new[] { 4, 5, 6 }, (x, y) => x + 3 == y));
        }
    }
}
