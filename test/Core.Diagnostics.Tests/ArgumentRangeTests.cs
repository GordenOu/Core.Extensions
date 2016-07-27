using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Diagnostics.Tests
{
    [TestClass]
    public class ArgumentRangeTests
    {
        [TestMethod]
        public void RangeNoException()
        {
            Requires.Range(1, string.Empty, true);
            Requires.Range<string>(null, string.Empty, true);
        }

        [TestMethod]
        public void ArgumenRangeThrowsException()
        {
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.Range<string>(null, string.Empty, false));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.Range(2.5, string.Empty, false));
        }

        [TestMethod]
        public void SpecialCases()
        {
            Requires.Positive((byte)1, string.Empty);
            Requires.Positive(4M, string.Empty);
            Requires.Positive(3.0, string.Empty);
            Requires.Positive((short)1, string.Empty);
            Requires.Positive(1, string.Empty);
            Requires.Positive(1L, string.Empty);
            Requires.Positive((sbyte)1, string.Empty);
            Requires.Positive(3.0F, string.Empty);
            Requires.Positive((ushort)2, string.Empty);
            Requires.Positive(2U, string.Empty);
            Requires.Positive(2UL, string.Empty);

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.Positive(0, string.Empty));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.Positive(0.0, string.Empty));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.Positive(-1, string.Empty));

            Requires.Negative(-1, string.Empty);
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.Negative(0, string.Empty));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.Negative(1, string.Empty));

            Requires.NonPositive(0, string.Empty);
            Requires.NonPositive(-1, string.Empty);
            Requires.NonPositive(-2.0, string.Empty);
            Requires.NonPositive(-3, string.Empty);
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.NonPositive(1, string.Empty));

            Requires.NonNegative(0, string.Empty);
            Requires.NonNegative(1, string.Empty);
            Requires.NonNegative(2.0, string.Empty);
            Requires.NonNegative(3, string.Empty);
            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.NonNegative(-1, string.Empty));
        }
    }
}
