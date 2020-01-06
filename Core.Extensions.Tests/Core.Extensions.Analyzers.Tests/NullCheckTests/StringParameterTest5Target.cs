using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class StringParameterTest5Target
    {
        public void Test(string a)
        {
            Requires.NotNullOrWhitespace(a, nameof(a));

        }
    }
}
