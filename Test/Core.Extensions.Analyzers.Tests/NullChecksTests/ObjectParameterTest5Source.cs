using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ObjectParameterTest5Source
    {
        public void Test(object a, object b, object c)
        {
            Requires.NotNull(b, nameof(b));

        }
    }
}
