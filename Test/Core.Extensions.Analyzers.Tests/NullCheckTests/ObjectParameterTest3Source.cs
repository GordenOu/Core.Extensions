using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ObjectParameterTest3Source
    {
        public void Test(object a, object b, object c)
        {
            Requires.NotNull(a, nameof(a));

        }
    }
}
