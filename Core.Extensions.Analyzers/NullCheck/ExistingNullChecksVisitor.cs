using System.Collections.Immutable;
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
                var arguments = operation.Arguments;
                var visitors = new IParameterMatchingSymbolVisitor[]
                {
                    new RequiresNullCheckMethodVisitor(arguments),
                    new DebugNullCheckMethodVisitor(arguments)
                };
                foreach (var visitor in visitors)
                {
                    visitor.Visit(operation.TargetMethod);
                    if (!(visitor.MatchedNullableParameter is null))
                    {
                        NullCheckParameterIndex = parameters.IndexOf(visitor.MatchedNullableParameter);
                        return;
                    }
                }
            }
        }

        public ImmutableArray<ExistingNullCheck> ExistingNullChecks { get; private set; }

        private ImmutableArray<IParameterSymbol> parameters;
        private readonly SemanticModel model;
        private readonly CancellationToken token;

        public ExistingNullChecksVisitor(SemanticModel model, CancellationToken token)
        {
            parameters = ImmutableArray<IParameterSymbol>.Empty;
            this.model = model;
            this.token = token;
            ExistingNullChecks = ImmutableArray<ExistingNullCheck>.Empty;
        }

        private void VisitBaseMethodDeclaration(BaseMethodDeclarationSyntax node)
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

        public override void VisitConstructorDeclaration(ConstructorDeclarationSyntax node)
        {
            VisitBaseMethodDeclaration(node);
        }

        public override void VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node)
        {
            VisitBaseMethodDeclaration(node);
        }

        public override void VisitMethodDeclaration(MethodDeclarationSyntax node)
        {
            VisitBaseMethodDeclaration(node);
        }

        public override void VisitOperatorDeclaration(OperatorDeclarationSyntax node)
        {
            VisitBaseMethodDeclaration(node);
        }

        public override void VisitLocalFunctionStatement(LocalFunctionStatementSyntax node)
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
                if (symbol is not null)
                {
                    builder.Add(symbol);
                }
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
