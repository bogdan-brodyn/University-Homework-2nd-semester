namespace Calculator;

using System.ComponentModel;

/// <summary>
/// Represents calculator model.
/// </summary>
public class CalculatorModel : INotifyPropertyChanged
{
    private int _firstOperand;
    private int _secondOperand;
    private Func<int, int, int>? _operator;
    private CalculatorState _state = new InitialState();
    private char _operatorChar;

    /// <inheritdoc/>
    public event PropertyChangedEventHandler? PropertyChanged;

    /// <summary>
    /// Gets current expression text (infix notation).
    /// </summary>
    public string CurrentExpressionText => GetCurrentExpressionText();

    /// <summary>
    /// Receives an external influence and produces a reaction.
    /// </summary>
    /// <param name="externalInfluenceChar">From the alphabet: [0..9], +, -, *, /, Less-than sign.</param>
    public void ReactToExternalInfluence(char externalInfluenceChar)
    {
        (_firstOperand, _secondOperand, _operator, _state) =
            _state.ReactToExternalInfluence(_firstOperand, _secondOperand, _operator, externalInfluenceChar);
        if (_state is OperatorState && externalInfluenceChar != '<')
        {
            _operatorChar = externalInfluenceChar;
        }

        NotifyPropertyChanged(nameof(CurrentExpressionText));
    }

    /// <summary>
    /// Produces current expression text (infix notation).
    /// </summary>
    /// <returns>Current expression text.</returns>
    /// <exception cref="Exception">Data structure integrity is violated.</exception>
    public string GetCurrentExpressionText()
    {
        if (_state is InitialState)
        {
            return string.Empty;
        }

        if (_state is FirstOperandState)
        {
            return _firstOperand.ToString();
        }

        if (_state is OperatorState)
        {
            return $"{_firstOperand} {_operatorChar}";
        }

        if (_state is SecondOperandState)
        {
            return $"{_firstOperand} {_operatorChar} {_secondOperand}";
        }

        throw new Exception("Data structure integrity is violated.");
    }

    private void NotifyPropertyChanged(string propertyName)
    {
        if (PropertyChanged is not null)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
