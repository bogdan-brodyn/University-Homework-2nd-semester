namespace Calculator;

/// <summary>
/// Represents calculator application's user interface.
/// </summary>
public class CalculatorForm : Form
{
    private readonly Label _label = new ();
    private readonly Button[,] _buttons = new Button[4, 4];
    private readonly string[,] _buttonTexts =
    {
        { "7", "8", "9", "/" },
        { "4", "5", "6", "*" },
        { "1", "2", "3", "-" },
        { "<", "0", "=", "+" },
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

    private void AddControls()
    {
        Controls.Add(_label);
        for (var y = 0; y < 4; ++y)
        {
            for (var x = 0; x < 4; ++x)
            {
                var button = new Button();
                button.Text = _buttonTexts[y, x];

                // button.Click +=
                Controls.Add(button);
                _buttons[x, y] = button;
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

        for (var y = 0; y < 4; ++y)
        {
            for (var x = 0; x < 4; ++x)
            {
                var button = _buttons[x, y];
                button.Location = new Point(buttonWidth * x, labelHeight + (buttonHeight * y));
                button.Size = new Size
                {
                    Width = x < 3 ? buttonWidth : Size.Width - (buttonWidth * 3),
                    Height = buttonHeight,
                };
            }
        }
    }
}
