using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class StringParameterTest3Target
    {
        public void Test(string a)
        {
            Debug.Assert(!(a is null));

        }
    }
}
