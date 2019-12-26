using System;
using System.Linq;
using Core.Extensions.Analyzers.NullCheck;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.Tests.NullCheckTests
{
    public abstract class NullCheckTest : PrefixedClassNameAnalyzerTest
    {
        public override Type AnalyzerType { get; } = typeof(NullCheckAnalyzer);

        public override Type CodeFixProviderType { get; } = typeof(AddRequiresNullCheckCodeFixProvider);

        public override SyntaxNode GetFixedNode(SyntaxNode root)
        {
            return root.DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Single();
        }

        public static ParameterSyntax GetParameter(SyntaxNode root, int index)
        {
            var methodDeclaration = root
                .DescendantNodes()
                .OfType<MethodDeclarationSyntax>()
                .Single();
            var parameter = methodDeclaration
                .DescendantNodes()
                .OfType<ParameterListSyntax>()
                .Single()
                .Parameters[index];
            return parameter;
        }
    }
}
