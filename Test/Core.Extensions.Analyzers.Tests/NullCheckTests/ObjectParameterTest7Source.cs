using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ObjectParameterTest7Source
    {
        public void Test(object a, object b, object c)
        {
            Requires.NotNull(c, nameof(c));

        }
    }
}
