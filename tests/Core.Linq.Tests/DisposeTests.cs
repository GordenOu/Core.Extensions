using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static System.Linq.Enumerable;

namespace Core.Linq.Tests
{
    public class Disposable : IDisposable
    {
        public Action DisposeAction { get; }

        public Disposable(Action disposeAction)
        {
            DisposeAction = disposeAction;
        }

        public void Dispose()
        {
            DisposeAction?.Invoke();
        }
    }

    [TestClass]
    public class DisposeTests
    {
        [TestMethod]
        public void Dispose()
        {
            int count = 0;
            var disposables = Range(0, 10).Select(i => new Disposable(() => count += 3));
            disposables.Dispose();
            Assert.AreEqual(30, count);
            disposables = null;
            Assert.ThrowsException<ArgumentNullException>(() => disposables.Dispose());
        }
    }
}
