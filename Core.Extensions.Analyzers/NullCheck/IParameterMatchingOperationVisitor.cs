using Microsoft.CodeAnalysis;

namespace Core.Extensions.Analyzers.NullCheck
{
    public interface IParameterMatchingOperationVisitor
    {
        public IParameterSymbol? MatchedNullableParameter { get; }

        public void Visit(IOperation operation);
    }
}
