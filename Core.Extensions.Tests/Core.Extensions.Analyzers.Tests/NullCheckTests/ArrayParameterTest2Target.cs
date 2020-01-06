using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ArrayParameterTest2Target
    {
        public void Test(int[] a)
        {
            Requires.NotNullOrEmpty(a, nameof(a));

        }
    }
}
