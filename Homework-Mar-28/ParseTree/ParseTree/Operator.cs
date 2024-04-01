namespace ParseTree;

/// <summary>
/// Implementation of one of the types of parse tree node - the operator.
/// </summary>
internal class Operator : IParseTreeNode
{
    private readonly Func<int, int, int> operation;
    private readonly IParseTreeNode leftOperand;
    private readonly IParseTreeNode rightOperand;

    /// <summary>
    /// Initializes a new instance of the <see cref="Operator"/> class.
    /// </summary>
    /// <param name="expression">Must follow the form described in the <see cref="ParseTree"/> constructor.</param>
    /// <param name="startPosition">Start position in the expression.</param>
    /// <param name="endPosition">End position in the expression.</param>
    internal Operator(string expression, int startPosition, out int endPosition)
    {
        var currentPosition = startPosition;
        InvalidExpressionException.ThrowIfUnexpectedChar(expression, currentPosition++, '(');
        operation = DefineOperation(expression, currentPosition++);
        InvalidExpressionException.ThrowIfUnexpectedChar(expression, currentPosition++, ' ');
        leftOperand = DefineOperand(expression, currentPosition, out currentPosition);
        InvalidExpressionException.ThrowIfUnexpectedChar(expression, currentPosition++, ' ');
        rightOperand = DefineOperand(expression, currentPosition, out currentPosition);
        InvalidExpressionException.ThrowIfUnexpectedChar(expression, currentPosition++, ')');
        endPosition = currentPosition;
    }

    private static IParseTreeNode DefineOperand(string expression, int startPosition, out int endPosition)
    {
        return expression[startPosition] == '('
            ? new Operator(expression, startPosition, out endPosition)
            : new Operand(expression, startPosition, out endPosition);
    }

    private static Func<int, int, int> DefineOperation(string expression, int position)
    {
        return expression[position] switch
        {
            '+' => (x, y) => x + y,
            '-' => (x, y) => x - y,
            '*' => (x, y) => x * y,
            '/' => (x, y) => x / y,
            _ => throw new InvalidOperationException(),
        };
    }
}