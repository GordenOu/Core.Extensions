using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using Core.Extensions.Analyzers.Resources;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace Core.Extensions.Analyzers.NullCheck
{
    [DiagnosticAnalyzer(LanguageNames.CSharp)]
    public class NullCheckAnalyzer : DiagnosticAnalyzer
    {
        public const string Id = nameof(NullCheck);

        private static readonly string title = Strings.AddRequiresNullCheckTitle;
        private static readonly string message = Strings.MissingNullCheckMessage;
        private static readonly string category = typeof(NullCheckAnalyzer).Namespace;

        public static DiagnosticDescriptor Descriptor { get; }
            = new DiagnosticDescriptor(
            id: Id,
            title: title,
            messageFormat: message,
            category: category,
            defaultSeverity: DiagnosticSeverity.Hidden,
            isEnabledByDefault: true);

        public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; }
            = ImmutableArray.Create(Descriptor);

        public static ImmutableArray<Diagnostic> GetDiagnostics(
            SyntaxNode node,
            SemanticModel model,
            CancellationToken token)
        {
            var nullableParametersVisitor = new NullableParametersVisitor(model, token);
            nullableParametersVisitor.Visit(node);
            var nullableParameters = nullableParametersVisitor.NullableParameters;

            var existingNullChecksVisitor = new ExistingNullChecksVisitor(model, token);
            existingNullChecksVisitor.Visit(node);
            var existingNullChecks = existingNullChecksVisitor.ExistingNullChecks;

            var builder = ImmutableArray.CreateBuilder<Diagnostic>();
            foreach (var nullableParameter in nullableParameters)
            {
                if (!existingNullChecks.Any(nullCheck => nullCheck.ParameterIndex == nullableParameter.Index))
                {
                    var diagnostic = Diagnostic.Create(
                        Descriptor,
                        nullableParameter.Syntax.Identifier.GetLocation(),
                        properties: ImmutableDictionary<string, string>.Empty.Add(
                            nameof(NullableParameter.Index),
                            nullableParameter.Index.ToString()));
                    builder.Add(diagnostic);
                }
            }
            return builder.ToImmutable();
        }

        private void AnalyzeSyntax(SyntaxNodeAnalysisContext context)
        {
            var semanticModel = context.SemanticModel;
            if (semanticModel is null)
            {
                return;
            }

            var diagnostics = GetDiagnostics(context.Node, semanticModel, context.CancellationToken);

            foreach (var diagnostic in diagnostics)
            {
                context.ReportDiagnostic(diagnostic);
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
