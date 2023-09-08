namespace TeraTerm2.test;

using Xunit;
using TeraTerm2;

public class TestScanner
{
    // Can parse empty string.
    [Fact]
    public void CanParseEmptyString()
    {
        var scanner = new Scanner("");
        var tokens = scanner.ScanTokens();
        Assert.Single(tokens);
        Assert.Equal(TokenType.EOF, tokens[0].Type);
    }
    
    // Can parse single character tokens.
    [Fact]
    public void CanParseSingleCharacterTokens()
    {
        var scanner = new Scanner("( ) { } ~ ! + - < >");
        var tokens = scanner.ScanTokens();
        Assert.Equal(10, tokens.Count);
        Assert.Equal(TokenType.LEFT_PAREN, tokens[0].Type);
        Assert.Equal(TokenType.RIGHT_PAREN, tokens[1].Type);
        Assert.Equal(TokenType.LEFT_BRACE, tokens[2].Type);
        Assert.Equal(TokenType.RIGHT_BRACE, tokens[3].Type);
        Assert.Equal(TokenType.TILDE, tokens[4].Type);
        Assert.Equal(TokenType.BANG, tokens[5].Type);
        Assert.Equal(TokenType.PLUS, tokens[6].Type);
        Assert.Equal(TokenType.MINUS, tokens[7].Type);
        Assert.Equal(TokenType.LT, tokens[8].Type);
        Assert.Equal(TokenType.GT, tokens[8].Type);
        Assert.Equal(TokenType.EOF, tokens[9].Type);
    }

    // Can parse multi-character tokens.
    [Fact]
    public void CanParseMultiCharacterTokens()
    {
        var scanner = new Scanner("<< <= <> ==");
        var tokens = scanner.ScanTokens();
        Assert.Equal(5, tokens.Count);
        Assert.Equal(TokenType.LTLT, tokens[0].Type);
        Assert.Equal(TokenType.LTE, tokens[1].Type);
        Assert.Equal(TokenType.LTGT, tokens[2].Type);
        Assert.Equal(TokenType.EQEQ, tokens[3].Type);
        Assert.Equal(TokenType.EOF, tokens[4].Type);
    }
    
    // Raises on invalid character sequence.
    [Fact]
    public void RaisesOnInvalidCharacterSequence()
    {
    }

    // Can skip comments.
    
    // Can parse variable names.
    
    // Can parse labels.
    
    // Can parse numbers.
    
    // Can parse strings.
    
    // Can parse keywords.
}