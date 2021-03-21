using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class StringParameterTest3Target
    {
        public void Test(int a, object b, string c, double[] d)
        {
            Debug.Assert(b is not null);
            Debug.Assert(c is not null);
            Debug.Assert(d is not null);

        }
    }
}
