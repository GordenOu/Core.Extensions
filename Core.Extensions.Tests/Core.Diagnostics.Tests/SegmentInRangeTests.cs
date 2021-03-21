using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Diagnostics.Tests
{
    [TestClass]
    public class SegmentInRangeTests
    {
        [TestMethod]
        public void ObjectArray()
        {
            Requires.SegmentInRange(Array.Empty<object>(), 0, 0);
            Requires.SegmentInRange(new object[5], 2, 3);

            Assert.ThrowsException<ArgumentNullException>(
                () => Requires.SegmentInRange<object>(null, 0, 0));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.SegmentInRange(new object[5], -1, -10));
        }

        [DataTestMethod]
        [DataRow(0, -1, 0)]
        [DataRow(0, 0, 1)]
        [DataRow(5, -1, 4)]
        [DataRow(5, 2, 4)]
        public void StringListThrows(int range, int offset, int count)
        {
            Assert.ThrowsException<SegmentNotInRangeException>(
                () => Requires.SegmentInRange(new string[range], offset, count));
        }
    }
}
