using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck;

/// <summary>
/// !string.IsNullOrEmpty(a) or !string.IsNullOrWhitespace(a)
/// </summary>
public class StringNullCheckOperationVisitor : OperationVisitor, IParameterMatchingOperationVisitor
{
    public IParameterSymbol? MatchedNullableParameter { get; private set; }

    public override void VisitUnaryOperator(IUnaryOperation operation)
    {
        if (operation.OperatorKind == UnaryOperatorKind.Not)
        {
            Visit(operation.Operand);
        }
    }

    public override void VisitInvocation(IInvocationOperation operation)
    {
        if (operation.Arguments.IsEmpty)
        {
            return;
        }

        var visitors = new IMethodMatchingVisitor[]
        {
                 new StringIsNullOrEmptyMethodVisitor(),
                 new StringIsNullOrWhitespaceMethodVisitor()
        };
        foreach (var visitor in visitors)
        {
            visitor.Visit(operation.TargetMethod);
            if (visitor.Matched)
            {
                Visit(operation.Arguments[0]);
                return;
            }
        }
    }

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
