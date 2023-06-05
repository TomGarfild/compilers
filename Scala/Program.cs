using Antlr4.Runtime.Tree;
using System;
using System.IO;

namespace Scala
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var sourceCode =
                File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"SourceCodes/class.scala"));
            var pathToSave = "./ast.txt";

            try
            {
                IParseTree tree = AstGenerator.CompileToTree(sourceCode, out var parser);
                using var sw = File.CreateText(pathToSave);
                AstGenerator.PrintTree(parser, tree, "", true, sw);
            }
            catch (SyntaxException ex)
            {
                Console.WriteLine($"Error parsing source code: {ex.Message}");
            }
        }
    }
}
