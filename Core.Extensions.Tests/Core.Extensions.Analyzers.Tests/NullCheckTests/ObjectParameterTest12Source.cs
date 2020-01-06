using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ObjectParameterTest12Source
    {
        public void Test(object a, object b, object c)
        {
            Requires.NotNull(a, nameof(a));
            Requires.NotNull(a, nameof(a));
            Requires.NotNull(c, nameof(c));
            Requires.NotNull(c, nameof(c));

        }
    }
}
