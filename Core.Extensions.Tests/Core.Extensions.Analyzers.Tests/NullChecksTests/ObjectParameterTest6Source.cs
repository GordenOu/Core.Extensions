using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ObjectParameterTest6Source
    {
        public void Test(object a, object b, object c)
        {
            Requires.NotNull(c, nameof(c));

        }
    }
}
