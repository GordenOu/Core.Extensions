using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class PointerParameterTest2Target
    {
        public unsafe void Test(int* a)
        {
            Debug.Assert(a is not null);

        }
    }
}
