namespace Calculator;

/// <summary>
/// Represents state of the calculator state machine.
/// </summary>
public class SecondOperandState : CalculatorState
{
    /// <inheritdoc/>
    public override (int FirstOperand, int SecondOperand, Func<int, int, int>? Operator, CalculatorState NewState) ReactToExternalInfluence(
        int firstOperand, int secondOperand, Func<int, int, int>? @operator, char externalInfluenceChar)
    {
        ArgumentNullException.ThrowIfNull(@operator);
        if (externalInfluenceChar - '0' >= 0 && externalInfluenceChar - '0' <= 9)
        {
            return (firstOperand, (secondOperand * 10) + externalInfluenceChar - '0', @operator, this);
        }

        if (externalInfluenceChar == '<')
        {
            return (secondOperand / 10) != 0
                ? (firstOperand, secondOperand / 10, @operator, this)
                : (firstOperand, 0, @operator, new OperatorState());
        }

        if (externalInfluenceChar == '=')
        {
            return (@operator(firstOperand, secondOperand), 0, null, new FirstOperandState());
        }

        var newOperator = RecognizeOperator(externalInfluenceChar);
        return (@operator(firstOperand, secondOperand), 0, newOperator, new OperatorState());
    }
}
