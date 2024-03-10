namespace StackCalculator;

/// <summary>
/// Stack implementation on pointers.
/// </summary>
/// <typeparam name="TValue">Stack element value.</typeparam>
public class StackOnPointers<TValue> : IStack<TValue>
{
    private Element? top;

    /// <inheritdoc/>
    public int Count { get; private set; }

    /// <inheritdoc/>
    /// <exception cref="InvalidOperationException">Stack is empty.</exception>
    public TValue Pop()
    {
        if (this.top is null)
        {
            throw new InvalidOperationException();
        }

        var topElementValue = this.top.Value;
        this.top = this.top.NextElement;
        this.Count--;
        return topElementValue;
    }

    /// <inheritdoc/>
    public void Push(TValue value)
    {
        this.top = new Element(value, this.top);
        this.Count++;
    }

    private class Element(TValue value, Element? nextElement)
    {
        public TValue Value { get; } = value;

        public Element? NextElement { get; } = nextElement;
    }
}
