using System.Collections.Immutable;
using System.Linq;
using Core.Extensions.Analyzers.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Core.Extensions.Analyzers.NullCheck
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class NullCheckAnalyzer : DiagnosticAnalyzer
    {
        public const string Id = nameof(NullCheckAnalyzer);

        private static readonly string title = Strings.AddNullCheckTitle;
        private static readonly string message = Strings.AddNullCheckMessage;
        private static readonly string category = typeof(NullCheckAnalyzer).Namespace;

        private static readonly DiagnosticDescriptor descriptor = new DiagnosticDescriptor(
            id: Id,
            title: title,
            messageFormat: message,
            category: category,
            defaultSeverity: DiagnosticSeverity.Hidden,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(descriptor);

        private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
        {
            var semanticModel = context.SemanticModel;
            if (semanticModel is null)
            {
                return;
            }

            var nullableParametersVisitor = new NullableParametersVisitor(semanticModel, context.CancellationToken);
            nullableParametersVisitor.Visit(context.Node);
            var nullableParameters = nullableParametersVisitor.NullableParameters;

            var existingNullChecksVisitor = new ExistingNullChecksVisitor(semanticModel, context.CancellationToken);
            existingNullChecksVisitor.Visit(context.Node);
            var existingNullChecks = existingNullChecksVisitor.ExistingNullChecks;

            foreach (var nullableParameter in nullableParameters)
            {
                if (!existingNullChecks.Any(nullCheck => nullCheck.ParameterIndex == nullableParameter.Index))
                {
                    var diagnostic = Diagnostic.Create(
                        descriptor,
                        nullableParameter.Syntax.Identifier.GetLocation(),
                        additionalLocations: new[] { context.Node.GetLocation() },
                        properties: ImmutableDictionary<string, string>.Empty.Add(
                            nameof(NullableParameter.Index),
                            nullableParameter.Index.ToString()));
                    context.ReportDiagnostic(diagnostic);
                }
            }
        }

        public override void Initialize(AnalysisContext context)
        {
            context.EnableConcurrentExecution();
            context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
            context.RegisterSyntaxNodeAction(AnalyzeSyntax, SyntaxKind.MethodDeclaration);
        }
    }
}
