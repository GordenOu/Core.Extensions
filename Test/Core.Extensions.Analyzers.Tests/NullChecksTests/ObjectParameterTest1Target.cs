using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ObjectParameterTest1Target
    {
        public void Test(object a, object b)
        {
            Requires.NotNull(a, nameof(a));
            Requires.NotNull(b, nameof(b));

        }
    }
}
