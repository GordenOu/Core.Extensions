namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public class GenericParameterTest1Source
    {
        public void Test<T>(T a)
            where T: class
        {

        }
    }
}
