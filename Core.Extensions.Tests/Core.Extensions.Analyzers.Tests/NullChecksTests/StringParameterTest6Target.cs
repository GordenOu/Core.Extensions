using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class StringParameterTest6Target
    {
        public void Test(int a, object b, string c, double[] d)
        {
            Debug.Assert(!(b is null));
            Debug.Assert(!string.IsNullOrWhiteSpace(c));
            Debug.Assert(!(d is null));

        }
    }
}
