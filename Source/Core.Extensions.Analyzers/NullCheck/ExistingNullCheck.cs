using Microsoft.CodeAnalysis;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class ExistingNullCheck
    {
        public int StatementIndex { get; }

        public int ParameterIndex { get; }

        public IOperation Statement { get; }

        public IParameterSymbol Parameter { get; }

        public ExistingNullCheck(
            int statementIndex,
            int parameterIndex,
            IOperation statement,
            IParameterSymbol parameter)
        {
            StatementIndex = statementIndex;
            ParameterIndex = parameterIndex;
            Statement = statement;
            Parameter = parameter;
        }
    }
}
