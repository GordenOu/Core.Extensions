using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Linq.Tests
{
    [TestClass]
    public class KeyValuePairExtensionsTests
    {
        [TestMethod]
        public void Deconstruct()
        {
            var pair = new KeyValuePair<string, int>("Test", 2);
            var (str, n) = pair;
            Assert.AreEqual("Test", str);
            Assert.AreEqual(2, n);

            var dictionary = new Dictionary<string, int>();
            var keys = new[] { "a", "b", "c", "d" };
            var values = new[] { 1, 2, 3, 4 };
            for (int i = 0; i < keys.Length; i++)
            {
                dictionary.Add(keys[i], values[i]);
            }

            int index = 0;
            foreach (var (key, value) in dictionary)
            {
                Assert.AreEqual(keys[index], key);
                Assert.AreSame(keys[index], key);
                Assert.AreEqual(values[index], value);

                index++;
            }
        }
    }
}
