using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class StringParameterTest2Target
    {
        public void Test(string a)
        {
            Requires.NotNullOrEmpty(a, nameof(a));

        }
    }
}
