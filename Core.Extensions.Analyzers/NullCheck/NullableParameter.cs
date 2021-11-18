using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.NullCheck;

public class NullableParameter
{
    public int Index { get; }

    public ParameterSyntax Syntax { get; }

    public IParameterSymbol Symbol { get; }

    public NullableParameter(int index, ParameterSyntax syntax, IParameterSymbol symbol)
    {
        Index = index;
        Syntax = syntax;
        Symbol = symbol;
    }
}
