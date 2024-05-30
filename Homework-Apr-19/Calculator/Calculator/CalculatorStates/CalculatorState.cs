namespace Calculator;

/// <summary>
/// Represents state of the calculator state machine.
/// </summary>
public abstract class CalculatorState
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorState"/> class.
    /// </summary>
    public CalculatorState()
    {
    }

    /// <summary>
    /// Recognizes operator by input char.
    /// </summary>
    /// <param name="inputChar">Operator char.</param>
    /// <returns>Operator function.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Char is not an operator.</exception>
    public static Func<int, int, int> RecognizeOperator(char inputChar) => inputChar switch
    {
        '+' => (x, y) => x + y,
        '-' => (x, y) => x - y,
        '*' => (x, y) => x * y,
        '/' => (x, y) => x / y,
        _ => throw new ArgumentOutOfRangeException(nameof(inputChar)),
    };

    /// <summary>
    /// Represents a reaction to an external influence.
    /// </summary>
    /// <param name="firstOperand">Defines the state. Calculator model firstOperand.</param>
    /// <param name="secondOperand">Defines the state. Calculator model secondOperand.</param>
    /// <param name="operator">Defines the state. Calculator model operator.</param>
    /// <param name="externalInfluenceChar">From the alphabet: [0..9], +, -, *, /, Less-than sign.</param>
    /// <returns>New state and answer.</returns>
    public virtual (int FirstOperand, int SecondOperand, Func<int, int, int>? Operator, CalculatorState NewState) ReactToExternalInfluence(
        int firstOperand, int secondOperand, Func<int, int, int>? @operator, char externalInfluenceChar) =>
            (firstOperand, secondOperand, @operator, this);
}
