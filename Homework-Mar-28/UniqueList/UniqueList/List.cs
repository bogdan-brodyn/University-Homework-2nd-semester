namespace UniqueList;

using System.Drawing;

/// <summary>
/// List data structure implementation.
/// </summary>
/// <typeparam name="TValue">Type of value the list contains.</typeparam>
public class List<TValue>
{
    private TValue?[] array = new TValue?[1];

    /// <summary>
    /// Gets or sets count of elements the list contains.
    /// </summary>
    public int Size { get; protected set; } = 0;

    /// <summary>
    /// Gets or sets the maximum count of elements the list can contain before resizing.
    /// </summary>
    public int Capacity { get; protected set; } = 1;

    /// <summary>
    /// Add a new element to the list end.
    /// </summary>
    /// <param name="value">New element's value.</param>
    public virtual void Add(TValue value)
    {
        if (Size >= Capacity)
        {
            Array.Resize(ref array, Capacity *= 2);
        }

        array[Size++] = value;
    }

    /// <summary>
    /// Remove an element at the list end.
    /// </summary>
    public void Remove()
    {
        if (Size < 1)
        {
            throw new RemoveFromEmptyListException();
        }

        array[--Size] = default;
    }

    /// <summary>
    /// Modify the value of an element the list contains.
    /// </summary>
    /// <param name="newValue">The new value of an element.</param>
    /// <param name="position">Element's position.</param>
    /// <exception cref="ArgumentOutOfRangeException">Position argument is out of range.</exception>
    public virtual void ModifyAt(TValue newValue, int position)
    {
        ThrowIfOutOfRange(position);
        array[position] = newValue;
    }

    /// <summary>
    /// Gets the value of an element the list contains.
    /// </summary>
    /// <param name="position">Element's position.</param>
    /// <returns>Element's value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Position argument is out of range.</exception>
    public TValue? GetBy(int position)
    {
        ThrowIfOutOfRange(position);
        return array[position];
    }

    /// <summary>
    /// Throws ArgumentOutOfRangeException if position is out of range.
    /// </summary>
    /// <param name="position">Position you want to check.</param>
    /// <exception cref="ArgumentOutOfRangeException">Position is out of range.</exception>
    protected void ThrowIfOutOfRange(int position)
    {
        if (position < 0 || position >= Size)
        {
            throw new ArgumentOutOfRangeException(nameof(position));
        }
    }
}
