using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class StringParameterTest6Target
    {
        public void Test(string a)
        {
            Debug.Assert(!string.IsNullOrWhiteSpace(a));

        }
    }
}
