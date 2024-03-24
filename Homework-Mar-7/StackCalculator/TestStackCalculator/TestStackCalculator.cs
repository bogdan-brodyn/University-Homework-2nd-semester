namespace TestStackCalculator;

using StackCalculator;

public class TestStackCalculator
{
    [TestCase("2", 2)]
    [TestCase("+2", 2)]
    [TestCase("-2", -2)]
    [TestCase("2 3 +", 5)]
    [TestCase("-2 +3 +", 1)]
    [TestCase("-2 -3 +", -5)]
    [TestCase("-2 -3 -", 1)]
    [TestCase("2 3 /", 2 / 3d)]
    [TestCase("2 3 *", 6)]
    [TestCase("1 2 3 + *", 5)]
    [TestCase("1 2 * 3 - 5 + 3 4 * /", 1 / 3d)]
    [TestCase("115 35 - -5 / 16 +", 0)]
    [TestCase("1 2 / 3 / 5 / 7 /", 1 / 210d)]
    public void Calculate_WithCorrectInputData_ShouldReturnExpectedResult(string expression, double expectedResult)
    {
        var actualResult1 = StackCalculator<StackOnList<double>>.Calculate(expression);
        Assert.That(Math.Abs(actualResult1 - expectedResult), Is.LessThan(1e-10));
        var actualResult2 = StackCalculator<StackOnPointers<double>>.Calculate(expression);
        Assert.That(Math.Abs(actualResult2 - expectedResult), Is.LessThan(1e-10));
    }

#pragma warning disable SA1515 // Single-line comment should be preceded by blank line
    // Empty string
    [TestCase("")]
    // Amount of operations does not match amount of numbers
    [TestCase("-")]
    [TestCase("0 -")]
    [TestCase("1 + 2")]
    [TestCase("1 2")]
    [TestCase("1 2 3")]
    // More than one space
    [TestCase("0  10 +")]
    [TestCase("1 202    3  + *")]
    // Not integers
    [TestCase("10.5")]
    [TestCase("a")]
    [TestCase("101a2 2 +")]
    [TestCase("afs.1? 2a53ara 3b + *")]
#pragma warning restore SA1515 // Single-line comment should be preceded by blank line
    public void Calculate_WithIncorrectInputData_ShouldThrowInvalidOperationException(string expression)
    {
        Assert.Throws<InvalidOperationException>(
            () => StackCalculator<StackOnList<double>>.Calculate(expression));
        Assert.Throws<InvalidOperationException>(
            () => StackCalculator<StackOnPointers<double>>.Calculate(expression));
    }

    [TestCase("1 0 /")]
    public void Calculate_WithIncorrectInputData_ShouldThrowDivideByZeroException(string expression)
    {
        Assert.Throws<DivideByZeroException>(
            () => StackCalculator<StackOnList<double>>.Calculate(expression));
        Assert.Throws<DivideByZeroException>(
            () => StackCalculator<StackOnPointers<double>>.Calculate(expression));
    }
}
