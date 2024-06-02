namespace ParseTree;

using System.Text;

/// <summary>
/// Interface for <see cref="ParseTree"/> nodes (i.e. <see cref="Operator"/>, <see cref="Operand"/>).
/// </summary>
internal interface IParseTreeNode
{
    /// <summary>
    /// Gets parse tree node value.
    /// </summary>
    int Value { get; }

    /// <summary>
    /// Append parse tree node string representation to expression.
    /// </summary>
    /// <param name="expression">Expression where you want to append it.</param>
    void AppendToExpression(StringBuilder expression);
}
