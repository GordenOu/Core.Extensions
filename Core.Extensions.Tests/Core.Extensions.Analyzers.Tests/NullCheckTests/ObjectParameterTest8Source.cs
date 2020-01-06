using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ObjectParameterTest8Source
    {
        public void Test(object a, object b, object c)
        {
            Requires.NotNull(c, nameof(c));

        }
    }
}
