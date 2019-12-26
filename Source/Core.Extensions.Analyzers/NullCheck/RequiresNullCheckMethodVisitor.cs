using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class RequiresNullCheckMethodVisitor : SymbolVisitor, IParameterMatchingSymbolVisitor
    {
        private ImmutableArray<IArgumentOperation> arguments;

        public IParameterSymbol? MatchedNullableParameter { get; private set; }

        private bool isNullCheckMethod = false;

        public RequiresNullCheckMethodVisitor(ImmutableArray<IArgumentOperation> arguments)
        {
            this.arguments = arguments;
        }

        public override void VisitMethod(IMethodSymbol symbol)
        {
            if (symbol.Name == "NotNull"
                || symbol.Name == "NotNullOrEmpty"
                || symbol.Name == "NotNullPtr")
            {
                Visit(symbol.ContainingType);
            }
            if (!isNullCheckMethod)
            {
                return;
            }

            if (arguments.IsEmpty)
            {
                return;
            }
            var argument = arguments[0];
            var visitor = new NullableParameterArgumentVisitor();
            visitor.Visit(argument);
            MatchedNullableParameter = visitor.MatchedNullableParameter;
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
                isNullCheckMethod = true;
            }
        }
    }
}
