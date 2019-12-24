using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class PointerParameterTest2Target
    {
        public unsafe void Test(object a, int b, int* c)
        {
            Requires.NotNull(a, nameof(a));
            Requires.NotNullPtr(c, nameof(c));

        }
    }
}
