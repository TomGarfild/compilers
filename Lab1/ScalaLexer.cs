using System.Text;

namespace Lexer;

public class ScalaLexer : Lexer
{
    private readonly TextReader _reader;
    private int _line;
    private int _column;

    public ScalaLexer(TextReader reader)
    {
        _reader = reader;
        _line = 1;
        _column = 1;
    }

    public override IEnumerable<Token> Tokenize()
    {
        Token token;
        do
        {
            token = NextToken();
            yield return token;
        } while (token.Type != TokenType.EndOfFile);
    }

    private Token NextToken()
    {
        while (true)
        {
            var @char = _reader.Peek();

            if (@char == -1)
            {
                return new Token(TokenType.EndOfFile, "<EOF>", _line, _column);
            }

            var currentChar = (char)@char;


            if (char.IsWhiteSpace(currentChar))
            {
                ReadChar();
                continue;
            }

            if (char.IsDigit(currentChar))
            {
                return RecognizeNumericConstant();
            }

            if (char.IsLetter(currentChar) || currentChar == '_')
            {
                return RecognizeIdentifierOrKeyword();
            }

            if (currentChar is '\'' or '\"')
            {
                return RecognizeLiteral();
            }

            if (currentChar == '/')
            {
                return RecognizeComment();
            }

            if (IsOperator(currentChar))
            {
                return RecognizeOperator();
            }

            return IsPunctuationMark(currentChar)
                ? RecognizePunctuationMark()
                : new Token(TokenType.Error, ReadChar().ToString(), _line, _column);
        }
    }

    private Token RecognizeIdentifierOrKeyword()
    {
        var sb = new StringBuilder();
        var startLine = _line;
        var startColumn = _column;

        while (char.IsLetterOrDigit((char)_reader.Peek()) || _reader.Peek() == '_')
        {
            sb.Append(ReadChar());
        }

        var value = sb.ToString();

        return IsKeyword(value)
            ? new Token(TokenType.Keyword, value, startLine, startColumn)
            : new Token(TokenType.Identifier, value, startLine, startColumn);
    }

    private Token RecognizeNumericConstant()
    {
        var sb = new StringBuilder();
        var startLine = _line;
        var startColumn = _column;

        while (char.IsDigit((char)_reader.Peek()))
        {
            sb.Append(ReadChar());
        }

        var value = sb.ToString();
        return new Token(TokenType.NumericConstant, value, startLine, startColumn);
    }

    private Token RecognizeLiteral()
    {
        var sb = new StringBuilder();
        var startLine = _line;
        var startColumn = _column;
        var quoteChar = ReadChar();

        while (_reader.Peek() != quoteChar && _reader.Peek() != -1)
        {
            sb.Append(ReadChar());
        }

        if (_reader.Peek() == quoteChar)
        {
            ReadChar(); // Consume closing quote
        }
        else
        {
            return new Token(TokenType.Error, $"Unterminated literal starting at line {_line}, column {_column}", startLine, startColumn);
        }

        var value = sb.ToString();
        return new Token(TokenType.Literal, value, startLine, startColumn);
    }

    private Token RecognizeComment()
    {
        var startLine = _line;
        var startColumn = _column;
        
        if (_reader.Peek() == '/')
        {
            ReadChar(); // Consume first '/'

            if (_reader.Peek() == '/')
            {
                // Line comment
                ReadChar(); // Consume second '/'

                var sb = new StringBuilder();

                while (_reader.Peek() != '\r' && _reader.Peek() != '\n' && _reader.Peek() != -1)
                {
                    sb.Append(ReadChar());
                }

                return new Token(TokenType.Comment, sb.ToString(), startLine, startColumn);
            }
            
            if (_reader.Peek() == '*')
            {
                // Block comment
                ReadChar(); // Consume '*'

                var sb = new StringBuilder();
                var commentClosed = false;

                while (_reader.Peek() != -1)
                {
                    var currentChar = ReadChar();

                    if (currentChar == '*' && _reader.Peek() == '/')
                    {
                        ReadChar(); // Consume '/'
                        commentClosed = true;
                        break;
                    }

                    sb.Append(currentChar);
                }

                return commentClosed
                    ? new Token(TokenType.Comment, sb.ToString(), startLine, startColumn)
                    : new Token(TokenType.Error, $"Unterminated block comment starting at line {_line}, column {_column}", startLine, startColumn);
            }
        }

        // Not a comment, treat as an operator
        return new Token(TokenType.Operator, "/", startLine, startColumn);
    }

    private Token RecognizeOperator()
    {
        var startLine = _line;
        var startColumn = _column;
        return new Token(TokenType.Operator, ReadChar().ToString(), startLine, startColumn);
    }

    private Token RecognizePunctuationMark()
    {
        var startLine = _line;
        var startColumn = _column;
        return new Token(TokenType.PunctuationMark, ReadChar().ToString(), startLine, startColumn);
    }

    private char ReadChar()
    {
        var currentChar = (char)_reader.Read();

        if (currentChar == '\r')
        {
            if (_reader.Peek() == '\n')
            {
                currentChar = (char)_reader.Read();
            }
            _line++;
            _column = 1;
        }
        else if (currentChar == '\n')
        {
            _line++;
            _column = 1;
        }
        else
        {
            _column++;
        }

        return currentChar;
    }
}