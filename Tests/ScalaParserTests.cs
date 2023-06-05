using Antlr4.Runtime.Tree;
using Scala;

namespace Tests;

public class ScalaParserTests
{
    [Theory]
    [InlineData("TestData/testcase1.scala")]
    [InlineData("TestData/testcase2.scala")]
    [InlineData("TestData/testcase3.scala")]
    [InlineData("TestData/testcase4.scala")]
    [InlineData("TestData/testcase5.scala")]
    [InlineData("TestData/testcase6.scala")]
    [InlineData("TestData/testcase7.scala")]
    [InlineData("TestData/testcase8.scala")]
    [InlineData("TestData/testcase9.scala")]
    [InlineData("TestData/testcase10.scala")]
    [InlineData("TestData/testcase11.scala")]
    [InlineData("TestData/testcase12.scala")]
    [InlineData("TestData/testcase13.scala")]
    [InlineData("TestData/testcase14.scala")]
    [InlineData("TestData/testcase15.scala")]
    [InlineData("TestData/testcase16.scala")]
    [InlineData("TestData/testcase17.scala")]
    [InlineData("TestData/testcase18.scala")]
    [InlineData("TestData/testcase19.scala")]
    [InlineData("TestData/testcase20.scala")]
    [InlineData("TestData/testcase21.scala")]
    [InlineData("TestData/testcase22.scala")]
    [InlineData("TestData/testcase23.scala")]
    [InlineData("TestData/testcase24.scala")]
    [InlineData("TestData/testcase25.scala")]
    public void ParsesSourceCodeIntoAstTree(string code)
    {
        var sourceCode = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, code));
        var tree = AstGenerator.CompileToTree(sourceCode, out var parser);
        

        Assert.NotNull(tree);
        Assert.Equal(0, parser.NumberOfSyntaxErrors);
    }

    [Fact]
    public void ParsesSourceCodeShouldThrowSyntaxErrorWhenThereIsAnError()
    {
        var sourceCode = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, @"TestData/testcase_error.scala"));
        IParseTree Action() => AstGenerator.CompileToTree(sourceCode, out var parser);

        Assert.Throws<SyntaxException>((Func<IParseTree>)Action);
    }
}