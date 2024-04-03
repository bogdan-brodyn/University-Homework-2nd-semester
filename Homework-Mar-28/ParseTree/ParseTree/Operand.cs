namespace ParseTree;

using System.Text;

/// <summary>
/// Implementation of one of the types of parse tree node - the operand.
/// </summary>
internal class Operand : IParseTreeNode
{
    private readonly int value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Operand"/> class.
    /// </summary>
    /// <param name="expression">An expression of the form described in the <see cref="ParseTree"/> constructor.</param>
    /// <param name="startPosition">Start position in the expression.</param>
    /// <param name="endPosition">End position in the expression.</param>
    internal Operand(string expression, int startPosition, out int endPosition)
    {
        endPosition = expression.IndexOf(' ', startPosition);
        if (endPosition == -1)
        {
            throw new InvalidExpressionException();
        }

        var convertedSuccessfully = int.TryParse(
            expression.AsSpan(
                startPosition,
                endPosition - startPosition + 1),
            out value);
        if (!convertedSuccessfully)
        {
            throw new InvalidOperationException();
        }
    }

    /// <inheritdoc/>
    int IParseTreeNode.Value => value;

    /// <inheritdoc/>
    void IParseTreeNode.AppendToExpression(StringBuilder expression)
        => expression.AppendLine(value.ToString());
}
