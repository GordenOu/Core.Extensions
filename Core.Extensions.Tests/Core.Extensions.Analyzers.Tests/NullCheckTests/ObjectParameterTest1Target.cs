using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class ObjectParameterTest1Target
    {
        public void Test(object a)
        {
            Requires.NotNull(a, nameof(a));

        }
    }
}
