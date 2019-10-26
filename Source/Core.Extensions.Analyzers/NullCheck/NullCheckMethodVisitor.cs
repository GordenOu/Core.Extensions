using Microsoft.CodeAnalysis;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class NullCheckMethodVisitor : SymbolVisitor
    {
        public bool IsNullCheckMethod { get; private set; }

        public override void Visit(ISymbol symbol)
        {
            IsNullCheckMethod = false;
            base.Visit(symbol);
        }

        public override void VisitMethod(IMethodSymbol symbol)
        {
            if (symbol.Name == "NotNull")
            {
                Visit(symbol.ContainingType);
            }
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.Name == "Requires")
            {
                Visit(symbol.ContainingNamespace);
            }
        }

        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            if (symbol.ToDisplayString() == "Core.Diagnostics")
            {
                Visit(symbol.ContainingAssembly);
            }
        }

        public override void VisitAssembly(IAssemblySymbol symbol)
        {
            if (symbol.Name == "Core.Diagnostics")
            {
                IsNullCheckMethod = true;
            }
        }
    }
}
