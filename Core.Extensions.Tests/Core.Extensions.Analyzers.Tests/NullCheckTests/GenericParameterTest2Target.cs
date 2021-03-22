using System.Diagnostics;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class GenericParameterTest2Target
    {
        public void Test<T>(T a)
            where T: class
        {
            Debug.Assert(a is not null);

        }
    }
}
