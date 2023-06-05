namespace Lexer;

public record Token(TokenType Type, string Value, int Line, int Column)
{
    public override string ToString()
    {
        return $"Type: {Type}, Value: {Value}, Line: {Line}, Column: {Column}";
    }
}