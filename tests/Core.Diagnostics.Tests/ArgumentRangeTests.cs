using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Diagnostics.Tests
{
    [TestClass]
    public class ArgumentRangeTests
    {
        [TestMethod]
        public void ArgumentRangeNoException()
        {
            Requires.ArgumentRange(1, string.Empty)
                .GreaterThan(0)
                .NoGreaterThan(1)
                .LessThan(2)
                .NoLessThan(1);
            Requires.ArgumentRange(20, string.Empty)
                .GreaterThan(10)
                .NoGreaterThan(21)
                .LessThan(30)
                .NoLessThan(19);
        }

        [TestMethod]
        public void ArgumenRangeThrowsException()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => Requires.ArgumentRange<string>(null, string.Empty));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.ArgumentRange(2.5, string.Empty).GreaterThan(2.5));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
               () => Requires.ArgumentRange(3, string.Empty).GreaterThan(4));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.ArgumentRange(2.5, string.Empty).NoGreaterThan(2.4));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
               () => Requires.ArgumentRange(3, string.Empty).NoGreaterThan(2));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
                () => Requires.ArgumentRange(2.5, string.Empty).LessThan(2.5));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
               () => Requires.ArgumentRange(4, string.Empty).LessThan(3));

            Assert.ThrowsException<ArgumentOutOfRangeException>(
               () => Requires.ArgumentRange(2.5, string.Empty).NoLessThan(2.6));
            Assert.ThrowsException<ArgumentOutOfRangeException>(
               () => Requires.ArgumentRange(3, string.Empty).NoLessThan(4));
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
