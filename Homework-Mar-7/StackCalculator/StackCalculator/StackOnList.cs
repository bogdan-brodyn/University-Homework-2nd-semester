namespace StackCalculator;

/// <summary>
/// Stack implementation on list.
/// </summary>
/// <typeparam name="TValue">Stack element value.</typeparam>
public class StackOnList<TValue> : IStack<TValue>
{
    private readonly List<TValue> list = new ();

    /// <inheritdoc/>
    public int Count => list is not null ? list.Count : 0;

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public TValue Pop()
    {
        if (Count == 0)
        {
            throw new InvalidOperationException();
        }

        var topElementValue = list[Count - 1];
        list.RemoveAt(Count - 1);
        return topElementValue;
    }

    /// <inheritdoc/>
    public void Push(TValue value)
    {
        list.Add(value);
    }
}
