using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class ObjectParameterTest8Source
    {
        public void Test(object a, object b, object c, object d)
        {
            Requires.NotNull(a, nameof(a));
            Requires.NotNull(c, nameof(c));

        }
    }
}
