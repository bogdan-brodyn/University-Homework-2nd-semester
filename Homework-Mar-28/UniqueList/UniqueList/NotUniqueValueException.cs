namespace UniqueList;

/// <summary>
/// Exception created for UniqueList class.
/// </summary>
public class NotUniqueValueException : Exception
#pragma warning disable SA1600 // Elements should be documented
{
    public NotUniqueValueException()
    {
    }

    public NotUniqueValueException(string message)
        : base(message)
    {
    }
}
#pragma warning restore SA1600 // Elements should be documented
