using Microsoft.CodeAnalysis;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class StringIsNullOrWhitespaceMethodVisitor : SymbolVisitor, IMethodMatchingVisitor
    {
        public bool Matched { get; private set; } = false;

        public override void VisitMethod(IMethodSymbol symbol)
        {
            if (symbol.Name == nameof(string.IsNullOrWhiteSpace))
            {
                Visit(symbol.ContainingType);
            }
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.SpecialType == SpecialType.System_String)
            {
                Visit(symbol.ContainingNamespace);
            }
        }

        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            if (symbol.ToDisplayString() == nameof(System))
            {
                Matched = true;
            }
        }
    }
}
