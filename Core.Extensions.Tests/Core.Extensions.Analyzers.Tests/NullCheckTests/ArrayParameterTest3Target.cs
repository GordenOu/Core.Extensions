using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ArrayParameterTest3Target
    {
        public void Test(int[] a)
        {
            Debug.Assert(a is not null);

        }
    }
}
