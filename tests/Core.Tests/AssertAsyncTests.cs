using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests
{
    [TestClass]
    public class AssertAsyncTests
    {
        [TestMethod]
        public void NoException()
        {
            var exception = Assert.ThrowsException<AggregateException>(() =>
            {
                AssertAsync.ThrowsException<ArgumentException>(async () =>
                {
                    await Task.Delay(0);
                }).Wait();
            });
            Assert.IsInstanceOfType(exception.InnerException, typeof(AssertFailedException));
        }

        [TestMethod]
        public async Task ThrowsExceptionAsync()
        {
            await AssertAsync.ThrowsException<ArgumentException>(async () =>
            {
                await Task.Delay(0);
                throw new ArgumentException();
            });
        }
    }
}
