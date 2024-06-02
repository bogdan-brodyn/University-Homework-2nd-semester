namespace Calculator.Tests;

public class TestCalculatorModel
{
    [TestCase("", "")]
    [TestCase("0", "0")]
    [TestCase("+/-+***", "")]
    [TestCase("10+*-", "10 -")]
    [TestCase("112+0+150+1=", "263")]
    [TestCase("1+124125", "1 + 124125")]
    [TestCase("25000-5000/2/2*9+100", "45000 + 100")]
    [TestCase("200-1242+", "-1042 +")]
    [TestCase("99-200*21/3-", "-707 -")]
    [TestCase("<<<", "")]
    [TestCase("1000<<", "10")]
    [TestCase("123+20=</2*5=<", "3")]
    [TestCase("0+<", "0")]
    [TestCase("0+100<", "0 + 10")]
    public void TestCalculatorModel_WithCommonTestCases_MustReturnExpectedExpression(
        string testCaseExternalInfluences, string expectedExpressionText)
    {
        // Arrange
        var model = new CalculatorModel();

        // Act
        foreach (var externalInfluenceChar in testCaseExternalInfluences)
        {
            model.ReactToExternalInfluence(externalInfluenceChar);
        }

        // Assert
        var actualExpressionText = model.GetCurrentExpressionText();
        Assert.That(actualExpressionText, Is.EqualTo(expectedExpressionText));
    }

    [Test]
    public void TestCalculatorModel_WithIncorrectArgument_MustThrowArgumentOutOfRangeException()
    {
        var model = new CalculatorModel();
        Assert.Throws<ArgumentOutOfRangeException>(() => model.ReactToExternalInfluence(' '));
        Assert.Throws<ArgumentOutOfRangeException>(() => model.ReactToExternalInfluence('_'));
        Assert.Throws<ArgumentOutOfRangeException>(() => model.ReactToExternalInfluence('g'));
    }

    [Test]
    public void TestCalculatorModel_WithDivideByZeroTestCase_MustThrowDivideByZeroException()
    {
        var model = new CalculatorModel();
        model.ReactToExternalInfluence('1');
        model.ReactToExternalInfluence('/');
        model.ReactToExternalInfluence('0');
        Assert.Throws<DivideByZeroException>(() => model.ReactToExternalInfluence('='));
    }
}