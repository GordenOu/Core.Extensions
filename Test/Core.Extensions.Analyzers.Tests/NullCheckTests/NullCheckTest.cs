using System;
using System.Linq;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CodeActions;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public abstract class NullCheckTest : PrefixedClassNameAnalyzerTest
    {
        public override Type AnalyzerType { get; } = typeof(NullCheckAnalyzer);

        public override Type CodeFixProviderType { get; } = typeof(AddNullCheckCodeFixProvider);

        public override bool IsExpectedCodeFix(CodeAction action)
        {
            return action.Title == AddNullCheckCodeFixProvider.Title;
        }

        public override SyntaxNode GetFixedNode(SyntaxNode root)
        {
            return root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Single();
        }
    }
}
