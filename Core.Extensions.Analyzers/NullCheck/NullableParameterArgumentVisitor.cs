using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck;

public class NullableParameterArgumentVisitor : OperationVisitor
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
