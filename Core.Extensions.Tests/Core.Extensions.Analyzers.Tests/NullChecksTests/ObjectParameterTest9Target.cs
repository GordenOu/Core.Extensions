using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ObjectParameterTest9Target
    {
        public void Test(object a, object b, object c, object d)
        {
            Requires.NotNull(a, nameof(a));
            Requires.NotNull(b, nameof(b));
            Requires.NotNull(c, nameof(c));
            Requires.NotNull(d, nameof(d));

        }
    }
}
