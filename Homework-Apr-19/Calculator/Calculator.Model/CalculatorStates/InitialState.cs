namespace Calculator;

/// <summary>
/// Represents state of the calculator state machine.
/// </summary>
public class InitialState : CalculatorState
{
    /// <inheritdoc/>
    public override (int FirstOperand, int SecondOperand, Func<int, int, int>? Operator, CalculatorState NewState) ReactToExternalInfluence(
        int firstOperand, int secondOperand, Func<int, int, int>? @operator, char externalInfluenceChar)
    {
        if (externalInfluenceChar - '0' >= 0 && externalInfluenceChar - '0' <= 9)
        {
            return (externalInfluenceChar - '0', 0, null, new FirstOperandState());
        }

        if (externalInfluenceChar == '<')
        {
            return (0, 0, null, this);
        }

        if (externalInfluenceChar == '=')
        {
            return (0, 0, null, this);
        }

        @operator = RecognizeOperator(externalInfluenceChar);
        return (0, 0, null, this);
    }
}
