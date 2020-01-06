using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ArrayParameterTest1Target
    {
        public void Test(int a, object b, string c, double[] d)
        {
            Requires.NotNull(b, nameof(b));
            Requires.NotNull(c, nameof(c));
            Requires.NotNull(d, nameof(d));

        }
    }
}
