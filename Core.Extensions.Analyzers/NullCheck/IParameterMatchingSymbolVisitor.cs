using Microsoft.CodeAnalysis;

namespace Core.Extensions.Analyzers.NullCheck;

public interface IParameterMatchingSymbolVisitor
{
    public IParameterSymbol? MatchedNullableParameter { get; }

    public void Visit(ISymbol operation);
}
