using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class PointerParameterTest1Target
    {
        public unsafe void Test(int* a)
        {
            Requires.NotNullPtr(a, nameof(a));

        }
    }
}
