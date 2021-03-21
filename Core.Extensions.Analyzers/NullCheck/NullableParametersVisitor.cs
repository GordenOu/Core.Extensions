using System.Collections.Immutable;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class NullableParametersVisitor : CSharpSyntaxVisitor
    {
        public ImmutableArray<NullableParameter> NullableParameters { get; private set; }

        private readonly SemanticModel model;
        private readonly CancellationToken token;

        public NullableParametersVisitor(SemanticModel model, CancellationToken token)
        {
            this.model = model;
            this.token = token;
            NullableParameters = ImmutableArray<NullableParameter>.Empty;
        }

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            Visit(node.ParameterList);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            Visit(node.ParameterList);
        }

        public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
        {
            Visit(node.ParameterList);
        }

        public override void VisitParameterList(ParameterListSyntax node)
        {
            var parameters = node.Parameters;
            var builder = ImmutableArray.CreateBuilder<NullableParameter>();
            for (int i = 0; i < parameters.Count; i++)
            {
                var parameter = parameters[i];
                var symbol = model.GetDeclaredSymbol(parameter, token);
                if (symbol is null)
                {
                    continue;
                }
                var visitor = new NullableParameterVisitor();
                visitor.Visit(symbol);
                if (visitor.IsNullableParameter)
                {
                    builder.Add(new NullableParameter(i, parameter, symbol));
                }
            }
            NullableParameters = builder.ToImmutable();
        }
    }
}
