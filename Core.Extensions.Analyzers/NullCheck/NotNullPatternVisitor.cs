using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class NotNullPatternVisitor : OperationVisitor
    {
        public bool Matched { get; private set; } = false;

        public override void VisitNegatedPattern(INegatedPatternOperation operation)
        {
            Visit(operation.Pattern);
        }

        public override void VisitConstantPattern(IConstantPatternOperation operation)
        {
            Visit(operation.Value);
        }

        public override void VisitConversion(IConversionOperation operation)
        {
            Visit(operation.Operand);
        }

        public override void VisitLiteral(ILiteralOperation operation)
        {
            if (operation.ConstantValue.HasValue && operation.ConstantValue.Value is null)
            {
                Matched = true;
            }
        }
    }
}
