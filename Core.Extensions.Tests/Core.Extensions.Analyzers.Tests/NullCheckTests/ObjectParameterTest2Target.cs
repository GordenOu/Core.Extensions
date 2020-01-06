using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ObjectParameterTest2Target
    {
        public void Test(bool a, object b, int c)
        {
            Requires.NotNull(b, nameof(b));

        }
    }
}
