using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Linq.Enumerable;

namespace Core.Linq.Tests
{
    [TestClass]
    public class ApplyTests
    {
        [TestMethod]
        public void ApplyAction()
        {
            int[] array = null;
            Action<int> action = null;
            Assert.ThrowsException<ArgumentNullException>(() => array.Apply(i => array[0] += i));
            array = Range(1, 10).ToArray();
            Assert.ThrowsException<ArgumentNullException>(() => array.Apply(action));

            action = i => array[0] += i;
            array.Apply(action);
            CollectionAssert.AreEqual(new[] { 56, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, array);
        }

        private int TestAction(int value)
        {
            return value + 1;
        }

        private static T GenericTestAction<T>(int value)
        {
            return default;
        }

        [TestMethod]
        public void ApplyActionWithReturnValue()
        {
            int[] array = null;
            Func<int, int> action = null;
            Assert.ThrowsException<ArgumentNullException>(() => array.Apply(i => array[0] += i));
            array = Range(1, 10).ToArray();
            Assert.ThrowsException<ArgumentNullException>(() => array.Apply(action));

            action = i => array[0] += i;
            array.Apply(action);
            CollectionAssert.AreEqual(new[] { 56, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, array);

            array.Apply(TestAction);
            CollectionAssert.AreEqual(new[] { 56, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, array);

            array.Apply(GenericTestAction<int>);
            array.Apply(GenericTestAction<object>);
            array.Apply(GenericTestAction<string>);
            CollectionAssert.AreEqual(new[] { 56, 2, 3, 4, 5, 6, 7, 8, 9, 10 }, array);
        }

        [TestMethod]
        public void ApplyActionWithIndex()
        {
            int[] array = null;
            Action<int, int> action = null;
            Assert.ThrowsException<ArgumentNullException>(
                () => array.Apply((i, j) => array[0] += i * j));
            array = Repeat(1, 10).ToArray();
            Assert.ThrowsException<ArgumentNullException>(() => array.Apply(action));

            action = (i, j) => array[0] += i + j;
            array.Apply(action);
            CollectionAssert.AreEqual(new[] { 56, 1, 1, 1, 1, 1, 1, 1, 1, 1 }, array);
        }
    }
}
