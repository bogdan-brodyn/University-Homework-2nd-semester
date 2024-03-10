namespace StackCalculator;

/// <summary>
/// Stack implementation on list.
/// </summary>
/// <typeparam name="TValue">Stack element value.</typeparam>
public class StackOnList<TValue> : IStack<TValue>
{
    private readonly List<TValue> list = new ();

    /// <inheritdoc/>
    public int Count => this.list is not null ? this.list.Count : 0;

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public TValue Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var topElementValue = this.list[this.Count - 1];
        this.list.RemoveAt(this.Count - 1);
        return topElementValue;
    }

    /// <inheritdoc/>
    public void Push(TValue value)
    {
        this.list.Add(value);
    }
}
