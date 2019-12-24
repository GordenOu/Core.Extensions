using System.Collections.Immutable;
using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    [TestClass]
    public class ObjectParameterTest1 : NullCheckTest
    {
        public override Diagnostic[] GetExpectedDiagnostics(SyntaxNode root)
        {
            var parameter = GetParameter(root, 0);
            return new[]
            {
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, parameter.Identifier.GetLocation())
            };
        }

        public override bool IsExpectedCodeFix(CodeAction action, ImmutableArray<Diagnostic> diagnostics)
        {
            return action.Title == AddNullCheckCodeFixProvider.Title;
        }

        [TestMethod]
        public Task Test()
        {
            return base.Test();
        }
    }
}
