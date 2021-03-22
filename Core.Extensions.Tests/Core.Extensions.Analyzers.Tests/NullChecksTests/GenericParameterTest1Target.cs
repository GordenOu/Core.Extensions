using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class GenericParameterTest1Target
    {
        public unsafe void Test<T1, T2>(object a, T1 b, T2 c)
            where T2: class
        {
            Requires.NotNull(a, nameof(a));
            Requires.NotNull(c, nameof(c));

        }
    }
}
