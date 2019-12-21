using System.Linq;
using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    [TestClass]
    public class ObjectParameterTest1 : NullCheckTest
    {
        public override Diagnostic[] GetExpectedDiagnostics(SyntaxNode root)
        {
            var methodDeclaration = root
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Single();
            var parameter = methodDeclaration
                .DescendantNodes()
                .OfType<ParameterListSyntax>()
                .Single()
                .Parameters[0];
            return new[]
            {
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, parameter.Identifier.GetLocation())
            };
        }

        [TestMethod]
        public Task Test()
        {
            return base.Test();
        }
    }
}
