using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ArrayParameterTest2Target
    {
        public void Test(int a, object b, string c, double[] d)
        {
            Requires.NotNull(b, nameof(b));
            Requires.NotNullOrEmpty(c, nameof(c));
            Requires.NotNullOrEmpty(d, nameof(d));

        }
    }
}
