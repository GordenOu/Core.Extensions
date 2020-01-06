using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Diagnostics.Tests
{
    [TestClass]
    public class NullAndEmptyTests
    {
        [TestMethod]
        public unsafe void NotNull()
        {
            Requires.NotNull(new object(), string.Empty);
            Requires.NotNullPtr(new IntPtr(1).ToPointer(), string.Empty);

            string paramName;

            paramName = Assert.ThrowsException<ArgumentNullException>(
                () => Requires.NotNull<object>(null, "object")).ParamName;
            Assert.AreEqual("object", paramName);

            paramName = Assert.ThrowsException<ArgumentNullException>(
                () => Requires.NotNullPtr((void*)null, "pointer")).ParamName;
            Assert.AreEqual("pointer", paramName);
        }

        [TestMethod]
        public void NotNullOrEmpty()
        {
            Requires.NotNullOrEmpty(new[] { 1 }, string.Empty);
            Requires.NotNullOrEmpty(" ", string.Empty);

            string paramName;

            paramName = Assert.ThrowsException<ArgumentNullException>(
                () => Requires.NotNullOrEmpty<object>(null, "object")).ParamName;
            Assert.AreEqual("object", paramName);
            paramName = Assert.ThrowsException<ArgumentException>(
                () => Requires.NotNullOrEmpty(Array.Empty<object>(), "object")).ParamName;
            Assert.AreEqual("object", paramName);

            paramName = Assert.ThrowsException<ArgumentNullException>(
                () => Requires.NotNullOrEmpty(null, "string")).ParamName;
            Assert.AreEqual("string", paramName);
            paramName = Assert.ThrowsException<ArgumentException>(
                () => Requires.NotNullOrEmpty(string.Empty, "string")).ParamName;
            Assert.AreEqual("string", paramName);
        }


        [TestMethod]
        public void NotNullOrWhitespace()
        {
            Requires.NotNullOrWhitespace("a", string.Empty);

            string paramName;;

            paramName = Assert.ThrowsException<ArgumentNullException>(
                () => Requires.NotNullOrWhitespace(null, "string")).ParamName;
            Assert.AreEqual("string", paramName);
            paramName = Assert.ThrowsException<ArgumentException>(
                () => Requires.NotNullOrWhitespace(string.Empty, "string")).ParamName;
            Assert.AreEqual("string", paramName);
            paramName = Assert.ThrowsException<ArgumentException>(
                () => Requires.NotNullOrWhitespace(" ", "string")).ParamName;
            Assert.AreEqual("string", paramName);
        }

        [TestMethod]
        public void NotNullItems()
        {
            Requires.NotNullItems(new[] { new object() }, string.Empty);
            Requires.NotNullItems(new object[0], string.Empty);

            string paramName = Assert.ThrowsException<ArgumentNullException>(
                () => Requires.NotNullItems<object>(null, string.Empty)).ParamName;
            Assert.AreEqual("value", paramName);

            paramName = Assert.ThrowsException<ArgumentException>(
                () => Requires.NotNullItems(new object[1], "object")).ParamName;
            Assert.AreEqual("object", paramName);
        }
    }
}
