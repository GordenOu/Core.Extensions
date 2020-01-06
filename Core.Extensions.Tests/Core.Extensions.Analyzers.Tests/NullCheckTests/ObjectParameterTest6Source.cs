using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ObjectParameterTest6Source
    {
        public void Test(object a, object b, object c)
        {
            Requires.NotNull(b, nameof(b));

        }
    }
}
