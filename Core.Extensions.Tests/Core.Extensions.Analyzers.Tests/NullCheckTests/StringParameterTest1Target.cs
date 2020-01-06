using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class StringParameterTest1Target
    {
        public void Test(string a)
        {
            Requires.NotNull(a, nameof(a));

        }
    }
}
