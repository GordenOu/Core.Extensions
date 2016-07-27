using System;
using System.Threading.Tasks;
using Xunit;

namespace Core.Tests
{
    public static class AsyncAssert
    {
        public static Task<TException> ThrowsException<TException>(Func<Task> action)
            where TException : Exception
        {
            return Assert.ThrowsAsync<TException>(action);
        }
    }
}
