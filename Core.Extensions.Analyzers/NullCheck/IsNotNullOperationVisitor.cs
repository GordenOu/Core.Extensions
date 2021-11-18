using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck;

/// <summary>
/// a is not null
/// </summary>
public class IsNotNullOperationVisitor : OperationVisitor, IParameterMatchingOperationVisitor
{
    public IParameterSymbol? MatchedNullableParameter { get; private set; }

    public override void VisitIsPattern(IIsPatternOperation operation)
    {
        var visitor = new NotNullPatternVisitor();
        visitor.Visit(operation.Pattern);
        if (visitor.Matched)
        {
            Visit(operation.Value);
        }
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
