using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class NullCheckMethodVisitor : SymbolVisitor
    {
        private class ArgumentVisitor : OperationVisitor
        {
            public IParameterSymbol? MatchedNullableParameter { get; private set; }

            public override void VisitArgument(IArgumentOperation operation)
            {
                Visit(operation.Value);
            }

            public override void VisitConversion(IConversionOperation operation)
            {
                Visit(operation.Operand);
            }

            public override void VisitParameterReference(IParameterReferenceOperation operation)
            {
                var visitor = new NullableParameterVisitor();
                visitor.Visit(operation.Parameter);
                if (visitor.IsNullableParameter)
                {
                    MatchedNullableParameter = operation.Parameter;
                }
            }
        }

        private ImmutableArray<IArgumentOperation> arguments;

        public bool IsNullCheckMethod { get; private set; }

        public NullCheckMethodVisitor(ImmutableArray<IArgumentOperation> arguments)
        {
            this.arguments = arguments;
        }

        public override void VisitMethod(IMethodSymbol symbol)
        {
            if (arguments.IsEmpty)
            {
                return;
            }
            var argument = arguments[0];
            var visitor = new ArgumentVisitor();
            visitor.Visit(argument);
            if (visitor.MatchedNullableParameter is null)
            {
                return;
            }
            if (visitor.MatchedNullableParameter.Type.Kind == SymbolKind.PointerType)
            {
                if (symbol.Name == "NotNullPtr")
                {
                    Visit(symbol.ContainingType);
                }
            }
            else if (symbol.Name == "NotNull")
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
