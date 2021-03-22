using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class GenericParameterTest1Target
    {
        public void Test<T>(T a)
            where T: class
        {
            Requires.NotNull(a, nameof(a));

        }
    }
}
