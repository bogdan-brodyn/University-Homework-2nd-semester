namespace StackCalculator;

/// <summary>
/// Сontract for the stack implementation.
/// </summary>
/// <typeparam name="TValue">Stack element type.</typeparam>
public interface IStack<TValue>
{
    /// <summary>
    /// Gets the number of elements contained in the Stack.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Removes and returns top Stack element.
    /// </summary>
    /// <returns>Removed top Stack element.</returns>
    public TValue Pop();

    /// <summary>
    /// Inserts new element at the Stack top.
    /// </summary>
    /// <param name="value">New element.</param>
    public void Push(TValue value);
}
