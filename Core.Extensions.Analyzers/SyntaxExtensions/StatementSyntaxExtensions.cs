using System;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Core.Extensions.Analyzers.SyntaxExtensions
{
    public static class StatementSyntaxExtensions
    {
        public static StatementSyntax InsertLeadingEndOfLine(this StatementSyntax statement)
        {
            var leadingTrivia = statement.GetLeadingTrivia();
            if (leadingTrivia.FirstOrDefault().IsKind(SyntaxKind.EndOfLineTrivia))
            {
                return statement;
            }
            else
            {
                leadingTrivia = leadingTrivia.Insert(0, SyntaxFactory.EndOfLine(Environment.NewLine));
                return statement.WithLeadingTrivia(leadingTrivia);
            }
        }
    }
}
