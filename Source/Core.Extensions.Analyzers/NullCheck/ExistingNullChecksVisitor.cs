using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers.NullCheck
{
    public class ExistingNullChecksVisitor : CSharpSyntaxVisitor
    {
        private class StatementVisitor : OperationVisitor
        {
            private readonly ImmutableArray<IParameterSymbol> parameters;

            public int NullCheckParameterIndex { get; private set; }

            public StatementVisitor(ImmutableArray<IParameterSymbol> parameters)
            {
                this.parameters = parameters;
                NullCheckParameterIndex = -1;
            }

            public override void VisitExpressionStatement(IExpressionStatementOperation operation)
            {
                Visit(operation.Operation);
            }

            public override void VisitInvocation(IInvocationOperation operation)
            {
                var notNullMethodVisitor = new NullCheckMethodVisitor(operation.Arguments);
                notNullMethodVisitor.Visit(operation.TargetMethod);
                if (notNullMethodVisitor.IsNullCheckMethod)
                {
                    Visit(operation.Arguments.FirstOrDefault());
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
                NullCheckParameterIndex = parameters.IndexOf(operation.Parameter);
            }
        }

        public ImmutableArray<ExistingNullCheck> ExistingNullChecks { get; private set; }

        private ImmutableArray<IParameterSymbol> parameters;
        private readonly SemanticModel model;
        private CancellationToken token;

        public ExistingNullChecksVisitor(SemanticModel model, CancellationToken token)
        {
            this.model = model;
            this.token = token;
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            if (node.Body is null)
            {
                return;
            }
            VisitParameterList(node.ParameterList);
            if (parameters.IsEmpty)
            {
                return;
            }
            VisitBlock(node.Body);
        }

        public override void VisitParameterList(ParameterListSyntax node)
        {
            var builder = ImmutableArray.CreateBuilder<IParameterSymbol>();
            foreach (var parameter in node.Parameters)
            {
                var symbol = model.GetDeclaredSymbol(parameter, token);
                builder.Add(symbol);
            }
            parameters = builder.ToImmutable();
        }

        public override void VisitBlock(BlockSyntax node)
        {
            if (model.GetOperation(node, token) is IBlockOperation blockOperation)
            {
                var statements = blockOperation.Operations;
                var builder = ImmutableArray.CreateBuilder<ExistingNullCheck>();
                for (int i = 0; i < statements.Length; i++)
                {
                    var statement = statements[i];
                    var visitor = new StatementVisitor(parameters);
                    visitor.Visit(statement);
                    if (visitor.NullCheckParameterIndex >= 0)
                    {
                        builder.Add(
                            new ExistingNullCheck(
                                i,
                                visitor.NullCheckParameterIndex,
                                statement,
                                parameters[visitor.NullCheckParameterIndex]));
                    }
                }
                ExistingNullChecks = builder.ToImmutable();
            }
        }
    }
}
