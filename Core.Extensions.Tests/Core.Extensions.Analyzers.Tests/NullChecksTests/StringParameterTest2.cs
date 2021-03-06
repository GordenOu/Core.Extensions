using System;
using System.Collections.Immutable;
using System.Threading.Tasks;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Core.Extensions.Analyzers.Tests.NullChecksTests
{
    [TestClass]
    public class StringParameterTest2 : NullChecksTest
    {
        public override Type CodeFixProviderType { get; } = typeof(AddRequiresNullOrEmptyChecksCodeFixProvider);

        public override Diagnostic[] GetExpectedDiagnostics(SyntaxNode root)
        {
            var b = GetParameter(root, 1);
            var c = GetParameter(root, 2);
            var d = GetParameter(root, 3);
            return new[]
            {
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, b.Identifier.GetLocation()),
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, c.Identifier.GetLocation()),
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, d.Identifier.GetLocation())
            };
        }

        public override bool IsExpectedCodeFix(CodeAction action, ImmutableArray<Diagnostic> diagnostics)
        {
            if (action.Title != Strings.AddRequiresNullOrEmptyChecksTitle)
            {
                return false;
            }
            if (diagnostics.Length != 1)
            {
                return false;
            }
            if (diagnostics[0].Properties.TryGetValue(nameof(NullableParameter.Index), out string index))
            {
                return index == "2";
            }
            else
            {
                return false;
            }
        }

        [TestMethod]
        public Task Test()
        {
            return base.Test();
        }
    }
}
