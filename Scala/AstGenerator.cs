using Antlr4.Runtime.Tree;
using Antlr4.Runtime;
using System.IO;

namespace Scala
{
    public class AstGenerator
    {
        public static IParseTree CompileToTree(string sourceCode, out ScalaParser parser)
        {
            var inputStream = new AntlrInputStream(sourceCode);
            var lexer = new ScalaLexer(inputStream);
            var tokenStream = new CommonTokenStream(lexer);
            parser = new ScalaParser(tokenStream);

            parser.RemoveErrorListeners();
            parser.AddErrorListener(new ThrowExceptionErrorListener());

            return parser.compilationUnit();
        }

        internal static void PrintTree(IRecognizer parser, IParseTree tree, string indent, bool last, TextWriter sw)
        {
            sw.Write(indent);
            if (last)
            {
                sw.Write("\\-");
                indent += "  ";
            }
            else
            {
                sw.Write("|-");
                indent += "| ";
            }

            if (tree is RuleContext ruleNode)
            {
                sw.WriteLine(parser.RuleNames[ruleNode.RuleIndex]);
            }
            else
            {
                sw.WriteLine(tree.ToStringTree().Replace("(", " ( "));
            }
            for (var i = 0; i < tree.ChildCount; i++)
            {
                PrintTree(parser, tree.GetChild(i), indent, i == tree.ChildCount - 1, sw);
            }
        }
    }
}