namespace ParseTree;

/// <summary>
/// Parse tree deta structure implementation.
/// </summary>
public class ParseTree
{
    private readonly Operator root;

    /// <summary>
    /// Initializes a new instance of the <see cref="ParseTree"/> class.
    /// </summary>
    /// <param name="expression">An expression of the form (operation operand1 operand2)
    /// where operand may be the same kind expression or the number.</param>
    public ParseTree(string expression)
    {
        root = new Operator(expression, 0, out int endPosition);
        if (endPosition != expression.Length)
        {
            throw new InvalidExpressionException();
        }
    }
}
