using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck
{
    /// <summary>
    /// a != null
    /// </summary>
    public class NotEqualToNullOperationVisitor : OperationVisitor, IParameterMatchingOperationVisitor
    {
        public IParameterSymbol? MatchedNullableParameter { get; private set; }

        private bool isNullLiteralMatched = false;

        public override void VisitBinaryOperator(IBinaryOperation operation)
        {
            if (operation.OperatorKind == BinaryOperatorKind.NotEquals)
            {
                Visit(operation.RightOperand);
                if (isNullLiteralMatched)
                {
                    Visit(operation.LeftOperand);
                }
            }
        }

        public override void VisitConversion(IConversionOperation operation)
        {
            if (operation.ConstantValue.HasValue && operation.ConstantValue.Value is null)
            {
                isNullLiteralMatched = true;
            }
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
}
