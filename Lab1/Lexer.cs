namespace Lexer;

public abstract class Lexer
{
    private readonly HashSet<char> _operators = new()
    {
        '+', '-', '*', '/', '%', '=', '!', '<', '>', '&', '|', '^', '~'
    };

    private readonly HashSet<char> _punctuationMarks = new()
    {
        '.', ',', ';', ':', '(', ')', '{', '}', '[', ']'
    };

    private readonly HashSet<string> _keywords = new()
    {
        "abstract", "case", "catch", "class", "def", "do", "else", "extends", "false", "final", "finally", "for", "if",
        "implicit", "import", "match", "new", "null", "object", "override", "package", "private", "protected",
        "return", "sealed", "super", "this", "throw", "trait", "true", "try", "type", "val", "var", "while", "with", "yield"
    };
    
    protected bool IsOperator(char value)
    {
        return _operators.Contains(value);
    }

    protected bool IsPunctuationMark(char value)
    {
        return _punctuationMarks.Contains(value);
    }

    protected bool IsKeyword(string value)
    {
        return _keywords.Contains(value);
    }

    public abstract IEnumerable<Token> Tokenize();
}