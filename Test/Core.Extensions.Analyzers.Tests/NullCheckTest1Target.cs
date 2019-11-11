using Core.Diagnostics;

namespace Core.Extensions.Analyzers.Tests
{
    public class NullCheckTest1Target
    {
        public void Test(object a)
        {
            Requires.NotNull(a, nameof(a));

        }
    }
}
