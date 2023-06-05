namespace Lexer;

public enum TokenType
{
    Error,
    NumericConstant,
    Literal,
    SymbolicConstant,
    PreprocessorDirective,
    Comment,
    Keyword,
    Identifier,
    Operator,
    PunctuationMark,
    EndOfFile
}