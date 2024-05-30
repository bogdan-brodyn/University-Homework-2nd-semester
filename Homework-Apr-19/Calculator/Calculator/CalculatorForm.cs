namespace Calculator;

/// <summary>
/// Represents calculator application's user interface.
/// </summary>
public class CalculatorForm : Form
{
    private readonly CalculatorModel _model = new ();
    private readonly Label _label = new ();
    private readonly Button[,] _buttons = new Button[4, 4];
    private readonly char[,] _buttonInfluenceToModel =
    {
        { '7', '8', '9', '/' },
        { '4', '5', '6', '*' },
        { '1', '2', '3', '-' },
        { '<', '0', '=', '+' },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="CalculatorForm"/> class.
    /// </summary>
    public CalculatorForm()
    {
        Name = "Calculator";
        StartPosition = FormStartPosition.CenterScreen;
        Size = new Size(500, 550);
        MinimumSize = Size;
        Load += (sender, args) => AddControls();
        Load += (sender, args) => ResizeControls();
        Resize += (sender, args) => ResizeControls();
    }

    private void RewriteLabelText()
    {
        _label.Text = _model.GetCurrentExpressionText();
    }

    private void AddControls()
    {
        Controls.Add(_label);
        for (var row = 0; row < 4; ++row)
        {
            for (var column = 0; column < 4; ++column)
            {
                var button = new Button();
                var buttonInfluenceChar = _buttonInfluenceToModel[row, column];
                button.Text = buttonInfluenceChar.ToString();
                button.Click += (sender, args) => _model.ReactToExternalInfluence(buttonInfluenceChar);
                button.Click += (sender, args) => RewriteLabelText();
                _buttons[row, column] = button;
                Controls.Add(button);
            }
        }
    }

    private void ResizeControls()
    {
        var minLabelHeight = 50;
        var buttonWidth = Size.Width / 4;
        var buttonHeight = (Size.Height - minLabelHeight) / 4;
        var labelHeight = Size.Height - (buttonHeight * 4);

        _label.Location = new Point(0, 0);
        _label.Size = new Size(Size.Width, labelHeight);

        for (var row = 0; row < 4; ++row)
        {
            for (var column = 0; column < 4; ++column)
            {
                var button = _buttons[row, column];
                button.Location = new Point(buttonWidth * column, labelHeight + (buttonHeight * row));
                button.Size = new Size
                {
                    Width = column < 3 ? buttonWidth : Size.Width - (buttonWidth * 3),
                    Height = buttonHeight,
                };
            }
        }
    }
}
