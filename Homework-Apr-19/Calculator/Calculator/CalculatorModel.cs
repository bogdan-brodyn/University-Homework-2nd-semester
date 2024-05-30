namespace Calculator;

/// <summary>
/// Represents calculator model.
/// </summary>
public class CalculatorModel
{
    private int _firstOperand = 0;
    private int _secondOperand = 0;
    private Func<int, int, int>? _operator = null;
    private CalculatorState _state = new InitialState();

    /// <summary>
    /// Receives an external influence and produces a reaction.
    /// </summary>
    /// <param name="externalInfluenceChar">From the alphabet: [0..9], +, -, *, /, Less-than sign.</param>
    public void ReactToExternalInfluence(char externalInfluenceChar)
    {
        (_firstOperand, _secondOperand, _operator, _state) =
            _state.ReactToExternalInfluence(_firstOperand, _secondOperand, _operator, externalInfluenceChar);
    }
}
