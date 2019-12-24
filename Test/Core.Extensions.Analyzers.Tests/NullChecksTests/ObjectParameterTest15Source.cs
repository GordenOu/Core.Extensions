using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ObjectParameterTest15Source
    {
        public void Test(object a, object b, object c, object d)
        {
            Requires.NotNull(d, nameof(d));
            Requires.NotNull(b, nameof(b));

        }
    }
}
