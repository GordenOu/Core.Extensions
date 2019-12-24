using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ObjectParameterTest13Source
    {
        public void Test(object a, object b, object c, object d)
        {
            Requires.NotNull(b, nameof(b));
            Requires.NotNull(d, nameof(d));

        }
    }
}
