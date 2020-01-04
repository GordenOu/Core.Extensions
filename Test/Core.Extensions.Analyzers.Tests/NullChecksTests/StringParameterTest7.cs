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
    public class StringParameterTest7 : NullChecksTest
    {
        public override Diagnostic[] GetExpectedDiagnostics(SyntaxNode root)
        {
            var o3 = GetParameter(root, 3);
            var s7 = GetParameter(root, 10);
            var d4 = GetParameter(root, 14);
            var p3 = GetParameter(root, 17);
            return new[]
            {
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, o3.Identifier.GetLocation()),
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, s7.Identifier.GetLocation()),
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, d4.Identifier.GetLocation()),
                Diagnostic.Create(NullCheckAnalyzer.Descriptor, p3.Identifier.GetLocation()),
            };
        }

        public override bool IsExpectedCodeFix(CodeAction action, ImmutableArray<Diagnostic> diagnostics)
        {
            if (action.Title != Strings.AddRequiresNullChecksTitle)
            {
                return false;
            }
            if (diagnostics.Length != 1)
            {
                return false;
            }
            if (diagnostics[0].Properties.TryGetValue(nameof(NullableParameter.Index), out string index))
            {
                return index == "10";
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
