namespace ParseTree.Tests;

public class TestParseTree
{
    [TestCase("(+ 1 2)", "(1 + 2)", 3)]
    [TestCase("(- 102 223)", "(102 - 223)", -121)]
    [TestCase("(* 12134 -1)", "(12134 * -1)", -12134)]
    [TestCase("(/ 1 2)", "(1 / 2)", 0)]
    [TestCase("(+ 2 (* 2 2))", "(2 + (2 * 2))", 6)]
    [TestCase("(/ (+ 234 (- 1001 1)) 2)", "((234 + (1001 - 1)) / 2)", 617)]
    [TestCase("(+ (+ 1 (+ 1 1)) (+ (+ 1 1) 1))", "((1 + (1 + 1)) + ((1 + 1) + 1))", 6)]
    [TestCase("(/ (+ (- (/ (+ (- 1 2) 3) 2) 4) 5) 2)", "((((((1 - 2) + 3) / 2) - 4) + 5) / 2)", 1)]
    public void TestParseTreeMethods_WithCorrectInputExpression_ShouldReturnExpectedResults(
        string inputExpression,
        string expectedGetExpressionInInfixNotationResult,
        int expectedComputeResult)
    {
        var parseTree = new ParseTree(inputExpression);
        Assert.That(
            parseTree.GetExpressionInInfixNotation(),
            Is.EqualTo(expectedGetExpressionInInfixNotationResult));
        Assert.That(parseTree.Compute(), Is.EqualTo(expectedComputeResult));
    }

    [TestCase("")]
    [TestCase("(")]
    [TestCase("()")]
    [TestCase("(+ )")]
    [TestCase("(- 1, 2)")]
    [TestCase("(+ 1 2")]
    [TestCase("(/ (+ 1 1) (* 1))")]
    [TestCase("(. 1.2)")]
    [TestCase("(a w f)")]
    [TestCase("asfsgdsdg")]
    public void TestParseTreeConstructor_WithIncorrectInputExpression_ShouldThrowInvalidExpressionException(
        string inputExpression)
    {
        Assert.Throws<InvalidExpressionException>(() => new ParseTree(inputExpression));
    }

    [TestCase("(/ 1 0)")]
    [TestCase("(/ 0 (+ 1 -1))")]
    [TestCase("(/ (- 1 2) (* 114 0))")]
    public void TestParseTreeConstructor_WithIncorrectInputExpression_ShouldThrowDivideByZeroException(
        string inputExpression)
    {
        Assert.Throws<DivideByZeroException>(() => new ParseTree(inputExpression));
    }
}