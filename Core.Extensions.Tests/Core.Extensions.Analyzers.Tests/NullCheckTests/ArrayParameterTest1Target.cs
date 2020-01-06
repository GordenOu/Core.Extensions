using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ArrayParameterTest1Target
    {
        public void Test(int[] a)
        {
            Requires.NotNull(a, nameof(a));

        }
    }
}
