namespace Kruskal;

/// <summary>
/// Represents errors related to invalid graph representation.
/// </summary>
public class InvalidGraphRepresentationException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidGraphRepresentationException"/> class.
    /// </summary>
    public InvalidGraphRepresentationException()
        : base()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidGraphRepresentationException"/> class.
    /// </summary>
    /// <param name="message">Specified error message.</param>
    public InvalidGraphRepresentationException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidGraphRepresentationException"/> class.
    /// </summary>
    /// <param name="message">Specified error message.</param>
    /// <param name="innerException">Reference to the inner exception that is the cause of this exception.</param>
    public InvalidGraphRepresentationException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Throws <see cref="InvalidGraphRepresentationException"/> if actual char is unexpected.
    /// </summary>
    /// <param name="stringToBeChecked">Reference to the string you want to check.</param>
    /// <param name="charToBeCheckedPosition">Index of char to be checked.</param>
    /// <param name="expectedChar">Char you expect.</param>
    /// <exception cref="InvalidGraphRepresentationException">Actual char is unexpected.</exception>
    public static void ThrowIfUnexpectedChar(
        string stringToBeChecked,
        int charToBeCheckedPosition,
        char expectedChar)
    {
        if (charToBeCheckedPosition >= stringToBeChecked.Length)
        {
            throw new InvalidGraphRepresentationException(
                $"'{expectedChar}' was expected but the line ended.");
        }

        var currentChar = stringToBeChecked[charToBeCheckedPosition];
        if (currentChar != expectedChar)
        {
            throw new InvalidGraphRepresentationException(
                $"'{expectedChar}' was expected but actually there was '{currentChar}'.");
        }
    }
}
