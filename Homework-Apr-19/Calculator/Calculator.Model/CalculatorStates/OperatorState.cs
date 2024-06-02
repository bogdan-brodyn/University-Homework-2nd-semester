namespace Calculator;

/// <summary>
/// Represents state of the calculator state machine.
/// </summary>
public class OperatorState : CalculatorState
{
    /// <inheritdoc/>
    public override (int FirstOperand, int SecondOperand, Func<int, int, int>? Operator, CalculatorState NewState) ReactToExternalInfluence(
        int firstOperand, int secondOperand, Func<int, int, int>? @operator, char externalInfluenceChar)
    {
        ArgumentNullException.ThrowIfNull(@operator);
        if (externalInfluenceChar - '0' >= 0 && externalInfluenceChar - '0' <= 9)
        {
            return (firstOperand, externalInfluenceChar - '0', @operator, new SecondOperandState());
        }

        if (externalInfluenceChar == '<')
        {
            return (firstOperand, 0, null, new FirstOperandState());
        }

        if (externalInfluenceChar == '=')
        {
            return (firstOperand, 0, null, new FirstOperandState());
        }

        @operator = RecognizeOperator(externalInfluenceChar);
        return (firstOperand, 0, @operator, new OperatorState());
    }
}
