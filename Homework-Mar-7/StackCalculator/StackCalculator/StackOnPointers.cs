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
        if (top is null)
        {
            throw new InvalidOperationException();
        }

        var topElementValue = top.Value;
        top = top.NextElement;
        Count--;
        return topElementValue;
    }

    /// <inheritdoc/>
    public void Push(TValue value)
    {
        top = new Element(value, top);
        Count++;
    }

    private class Element(TValue value, Element? nextElement)
    {
        public TValue Value { get; } = value;

        public Element? NextElement { get; } = nextElement;
    }
}
