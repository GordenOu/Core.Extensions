using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ObjectParameterTest4Target
    {
        public void Test(object a, object b, object c)
        {
            Requires.NotNull(a, nameof(a));
            Requires.NotNull(b, nameof(b));
            Requires.NotNull(c, nameof(c));

        }
    }
}
