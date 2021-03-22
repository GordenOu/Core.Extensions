namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    public class GenericParameterTest1Source
    {
        public unsafe void Test<T1, T2>(object a, T1 b, T2 c)
            where T2: class
        {

        }
    }
}
