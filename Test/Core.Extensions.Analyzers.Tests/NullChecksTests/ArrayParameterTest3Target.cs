using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ArrayParameterTest3Target
    {
        public void Test(int a, object b, string c, double[] d)
        {
            Debug.Assert(!(b is null));
            Debug.Assert(!(c is null));
            Debug.Assert(!(d is null));

        }
    }
}
