namespace UniqueList;

/// <summary>
/// Exception created for List class.
/// </summary>
public class RemoveFromEmptyListException : Exception
#pragma warning disable SA1600 // Elements should be documented
{
    public RemoveFromEmptyListException()
    {
    }

    public RemoveFromEmptyListException(string message)
        : base(message)
    {
    }
}
#pragma warning restore SA1600 // Elements should be documented
