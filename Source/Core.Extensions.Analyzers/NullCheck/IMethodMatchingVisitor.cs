using Microsoft.CodeAnalysis;

namespace Core.Extensions.Analyzers.NullCheck
{
    public interface IMethodMatchingVisitor
    {
        public bool Matched { get; }

        public void Visit(ISymbol operation);
    }
}
