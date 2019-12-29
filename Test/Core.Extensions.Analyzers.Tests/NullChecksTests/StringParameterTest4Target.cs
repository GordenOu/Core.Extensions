using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class StringParameterTest4Target
    {
        public void Test(int a, object b, string c, double[] d)
        {
            Debug.Assert(!(b is null));
            Debug.Assert(!string.IsNullOrEmpty(c));
            Debug.Assert(!(d is null));

        }
    }
}
