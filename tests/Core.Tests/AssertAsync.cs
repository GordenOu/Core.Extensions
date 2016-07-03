using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Tests
{
    public static class AssertAsync
    {
        public static async Task<TException> ThrowsException<TException>(Func<Task> action)
            where TException : Exception
        {
            try
            {
                await action();
            }
            catch (TException exception)
            {
                return exception;
            }

            throw new AssertFailedException(
                $"No exception thrown. Expecting {typeof(TException).FullName}.");
        }
    }
}
