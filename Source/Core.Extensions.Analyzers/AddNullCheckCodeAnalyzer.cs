using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using Core.Extensions.Analyzers.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Operations;

namespace Core.Extensions.Analyzers
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class AddNullCheckCodeAnalyzer : DiagnosticAnalyzer
    {
        public const string Id = nameof(AddNullCheckCodeAnalyzer);

        private static readonly string title = Strings.AddNullCheckTitle;
        private static readonly string message = Strings.AddNullCheckMessage;
        private static readonly string category = typeof(AddNullCheckCodeAnalyzer).Namespace;

        private static readonly DiagnosticDescriptor descriptor = new DiagnosticDescriptor(
            id: Id,
            title: title,
            messageFormat: message,
            category: category,
            defaultSeverity: DiagnosticSeverity.Hidden,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(descriptor);

        private void AnalyzeParameterSyntax(SyntaxNodeAnalysisContext context)
        {
            if (!(context.Node is ParameterSyntax parameterSyntax))
            {
                return;
            }
            string parameterName = parameterSyntax.Identifier.Text;
            if (string.IsNullOrEmpty(parameterName))
            {
                return;
            }
            if (!(parameterSyntax.Parent?.Parent is MethodDeclarationSyntax methodDeclrationSyntax))
            {
                return;
            }
            var bodySyntax = methodDeclrationSyntax.Body;
            if (bodySyntax is null)
            {
                return;
            }

            var semanticModel = context.SemanticModel;
            if (semanticModel is null)
            {
                return;
            }
            var parameterSymbol = semanticModel.GetDeclaredSymbol(parameterSyntax, context.CancellationToken);
            if (parameterSymbol is null)
            {
                return;
            }
            if (parameterSymbol.Type?.IsReferenceType != true
                && parameterSymbol.Type?.OriginalDefinition.SpecialType != SpecialType.System_Nullable_T)
            {
                return;
            }
            var operation = semanticModel.GetOperation(bodySyntax, context.CancellationToken);
            if (!(operation is IBlockOperation blockOperation))
            {
                return;
            }

            var existingNullChecks = new List<IExpressionStatementOperation>();
            foreach (var statement in blockOperation.Operations)
            {
                if (statement is IExpressionStatementOperation expressionStatement
                    && expressionStatement.Operation is IInvocationOperation invocationOperation)
                {
                    var method = invocationOperation.TargetMethod;
                    var containingType = method?.ContainingType;
                    if (containingType?.ContainingNamespace.ToDisplayString() == "Core.Diagnostics"
                        && containingType?.Name == "Requires"
                        && method?.Name == "NotNull")
                    {
                        var firstArgument = invocationOperation.Arguments.FirstOrDefault();
                        if (firstArgument?.Value is IParameterReferenceOperation parameterReferenceOperation)
                        {
                            if (parameterSymbol.Equals(parameterReferenceOperation.Parameter))
                            {
                                return;
                            }
                            else
                            {
                                existingNullChecks.Add(expressionStatement);
                            }
                        }
                    }
                }
            }

            var additionalLocations = new List<Location> { bodySyntax.GetLocation() };
            foreach (var nullCheck in existingNullChecks)
            {
                additionalLocations.Add(nullCheck.Syntax.GetLocation());
            }
            var diagnostic = Diagnostic.Create(
                descriptor,
                parameterSyntax.Identifier.GetLocation(),
                additionalLocations);
            context.ReportDiagnostic(diagnostic);
        }

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(
                AnalyzeParameterSyntax,
                SyntaxKind.Parameter,
                SyntaxKind.MethodDeclaration);
        }
    }
}
