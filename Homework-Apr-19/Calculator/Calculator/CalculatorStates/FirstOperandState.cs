namespace Calculator;

/// <summary>
/// Represents state of the calculator state machine.
/// </summary>
public class FirstOperandState : CalculatorState
{
    /// <inheritdoc/>
    public override (int FirstOperand, int SecondOperand, Func<int, int, int>? Operator, CalculatorState NewState) ReactToExternalInfluence(
        int firstOperand, int secondOperand, Func<int, int, int>? @operator, char externalInfluenceChar)
    {
        if (externalInfluenceChar - '0' >= 0 && externalInfluenceChar - '0' <= 9)
        {
            return ((firstOperand * 10) + externalInfluenceChar - '0', 0, null, this);
        }

        if (externalInfluenceChar == '<')
        {
            return (firstOperand / 10) != 0
                ? (firstOperand / 10, 0, null, this)
                : (0, 0, null, new InitialState());
        }

        if (externalInfluenceChar == '=')
        {
            return (firstOperand, 0, null, this);
        }

        @operator = RecognizeOperator(externalInfluenceChar);
        return (firstOperand, 0, @operator, new OperatorState());
    }
}
