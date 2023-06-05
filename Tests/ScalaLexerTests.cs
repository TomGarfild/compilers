using Lexer;

namespace Tests;
public class ScalaLexerTests
{
    [Fact]
    public void TestNumericConstants()
    {
        var input = "123 456";
        var expectedTokens = new List<Token>
        {
            new(TokenType.NumericConstant, "123", 1, 1),
            new(TokenType.NumericConstant, "456", 1, 5),
            new(TokenType.EndOfFile, "<EOF>", 1, 8)
        };
        AssertTokens(input, expectedTokens);
    }

    [Fact]
    public void TestKeywordsAndIdentifiers()
    {
        var input = "val x = 10";
        var expectedTokens = new List<Token>
        {
            new(TokenType.Keyword, "val", 1, 1),
            new(TokenType.Identifier, "x", 1, 5),
            new(TokenType.Operator, "=", 1, 7),
            new(TokenType.NumericConstant, "10", 1, 9),
            new(TokenType.EndOfFile, "<EOF>", 1, 11)
        };
        AssertTokens(input, expectedTokens);
    }

    [Fact]
    public void TestLiteral()
    {
        var input = "\"Hello, World!\"";
        var expectedTokens = new List<Token>
        {
            new(TokenType.Literal, "Hello, World!", 1, 1),
            new(TokenType.EndOfFile, "<EOF>", 1, 16)
        };
        AssertTokens(input, expectedTokens);
    }

    [Fact]
    public void TestComment()
    {
        var input = "// This is a comment";
        var expectedTokens = new List<Token>
        {
            new(TokenType.Comment, " This is a comment", 1, 1),
            new(TokenType.EndOfFile, "<EOF>", 1, 21)
        };
        AssertTokens(input, expectedTokens);
    }

    [Fact]
    public void TestBlockComment()
    {
        var input = "/* This is a\r\n" +
                    "block comment*/";
        var expectedTokens = new List<Token>
        {
            new(TokenType.Comment, " This is a\nblock comment", 1, 1),
            new(TokenType.EndOfFile, "<EOF>", 2, 16)
        };
        AssertTokens(input, expectedTokens);
    }

    [Fact]
    public void TestErrorWhenUnterminatedLiteral()
    {
        var input = @"""Error";
        var expectedTokens = new List<Token>
        {
            new(TokenType.Error, "Unterminated literal starting at line 1, column 7", 1, 1),
            new(TokenType.EndOfFile, "<EOF>", 1, 7)
        };
        AssertTokens(input, expectedTokens);
    }

    [Fact]
    public void TestErrorWhenBlockCommentNotClosed()
    {
        var input = @"/*Error";
        var expectedTokens = new List<Token>
        {
            new(TokenType.Error, "Unterminated block comment starting at line 1, column 8", 1, 1),
            new(TokenType.EndOfFile, "<EOF>", 1, 8)
        };
        AssertTokens(input, expectedTokens);
    }

    private static void AssertTokens(string input, IReadOnlyList<Token> expectedTokens)
    {
        using var reader = new StringReader(input);
        var lexer = new ScalaLexer(reader);
        var tokens = lexer.Tokenize();

        var index = 0;
        foreach (var token in tokens)
        {
            Assert.True(index < expectedTokens.Count, $"Unexpected token at index {index}: {token}");
            Assert.Equal(expectedTokens[index], token);
            index++;
        }

        Assert.Equal(expectedTokens.Count, index);
    }
}