using System.Collections.Immutable;
using System.Diagnostics;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class DebugNullCheckMethodVisitor : SymbolVisitor, IParameterMatchingSymbolVisitor
    {
        private ImmutableArray<IArgumentOperation> arguments;

        public IParameterSymbol? MatchedNullableParameter { get; private set; }

        private bool isAssertMethod = false;

        public DebugNullCheckMethodVisitor(ImmutableArray<IArgumentOperation> arguments)
        {
            this.arguments = arguments;
        }

        public override void VisitMethod(IMethodSymbol symbol)
        {
            if (symbol.Name != nameof(Debug.Assert))
            {
                return;
            }
            Visit(symbol.ContainingType);
            if (!isAssertMethod)
            {
                return;
            }

            if (arguments.IsEmpty)
            {
                return;
            }
            var argument = arguments[0];
            var visitors = new IParameterMatchingOperationVisitor[]
            {
                new IsNotNullOperationVisitor(),
                new IsNotNullOrEmptyStringOperationVisitor(),
                new NotEqualToNullOperationVisitor()
            };
            foreach (var visitor in visitors)
            {
                visitor.Visit(argument.Value);
                if (!(visitor.MatchedNullableParameter is null))
                {
                    MatchedNullableParameter = visitor.MatchedNullableParameter;
                    return;
                }
            }
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.Name == nameof(Debug))
            {
                Visit(symbol.ContainingNamespace);
            }
        }

        public override void VisitNamespace(INamespaceSymbol symbol)
        {
            if (symbol.ToDisplayString() == typeof(Debug).Namespace)
            {
                isAssertMethod = true;
            }
        }
    }
}
