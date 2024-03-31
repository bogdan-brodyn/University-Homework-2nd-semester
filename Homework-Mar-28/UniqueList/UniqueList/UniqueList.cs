namespace UniqueList;

/// <summary>
/// Unique list data structure implementation where elements' values must be unique.
/// </summary>
/// <typeparam name="TValue">Type of value the list contains (must be IEquatable).</typeparam>
public class UniqueList<TValue> : List<TValue>
    where TValue : IEquatable<TValue>
{
    /// <summary>
    /// Add a new element to the list end.
    /// </summary>
    /// <param name="value">New element's value (must be unique).</param>\
    /// <exception cref="NotUniqueValueException">The value of a new element is not unique.</exception>
    public override void Add(TValue value)
    {
        for (var i = 0; i < Size; ++i)
        {
            if (Equals(value, GetBy(i)))
            {
                throw new NotUniqueValueException();
            }
        }

        base.Add(value);
    }

    /// <summary>
    /// Modify the value of an element the list contains.
    /// </summary>
    /// <param name="newValue">The new value of an element (should be unique).</param>
    /// <param name="position">Element's position.</param>
    /// <exception cref="ArgumentOutOfRangeException">Position argument is out of range.</exception>
    /// <exception cref="NotUniqueValueException">The new value of an element is not unique.</exception>
    public override void ModifyAt(TValue newValue, int position)
    {
        ThrowIfOutOfRange(position);
        if (Equals(newValue, GetBy(position)))
        {
            return;
        }

        for (var i = 0; i < Size; ++i)
        {
            if (Equals(newValue, GetBy(i)))
            {
                throw new NotUniqueValueException();
            }
        }

        base.ModifyAt(newValue, position);
    }
}
