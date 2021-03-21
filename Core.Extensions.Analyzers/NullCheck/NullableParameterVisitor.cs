using Microsoft.CodeAnalysis;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class NullableParameterVisitor : SymbolVisitor
    {
        public bool IsNullableParameter { get; private set; } = false;

        public override void VisitParameter(IParameterSymbol symbol)
        {
            Visit(symbol.Type);
        }

        public override void VisitArrayType(IArrayTypeSymbol symbol)
        {
            IsNullableParameter = true;
        }

        public override void VisitDynamicType(IDynamicTypeSymbol symbol)
        {
            IsNullableParameter = true;
        }

        public override void VisitNamedType(INamedTypeSymbol symbol)
        {
            if (symbol.IsReferenceType)
            {
                IsNullableParameter = true;
            }
        }

        public override void VisitPointerType(IPointerTypeSymbol symbol)
        {
            IsNullableParameter = true;
        }

        public override void VisitTypeParameter(ITypeParameterSymbol symbol)
        {
            if (symbol.HasReferenceTypeConstraint)
            {
                IsNullableParameter = true;
            }
        }
    }
}
