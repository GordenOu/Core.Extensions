using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class StringParameterTest4Target
    {
        public void Test(string a)
        {
            Debug.Assert(!string.IsNullOrEmpty(a));

        }
    }
}
