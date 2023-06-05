using Antlr4.Runtime;

namespace Scala
{
    public class ThrowExceptionErrorListener : BaseErrorListener
    {
        public override void SyntaxError(IRecognizer recognizer, IToken offendingSymbol, int line, int charPositionInLine, string msg, RecognitionException e)
        {
            throw new SyntaxException($"Syntax error at line {line}, position {charPositionInLine}: {msg}");
        }
    }

}