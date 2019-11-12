using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests
{
    public class NullCheckTest2Target
    {
        public unsafe void Test(int* a)
        {
            Requires.NotNullPtr(a, nameof(a));

        }
    }
}
