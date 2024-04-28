namespace ParseTree;

/// <summary>
/// Exception indicating invalid syntax in an expression.
/// </summary>
public class InvalidExpressionException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidExpressionException"/> class.
    /// </summary>
    public InvalidExpressionException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidExpressionException"/> class.
    /// </summary>
    /// <param name="message">Specified error message.</param>
    public InvalidExpressionException(string? message)
        : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="InvalidExpressionException"/> class.
    /// </summary>
    /// <param name="message">Specified error message.</param>
    /// <param name="innerException">Reference to the inner exception that is the cause of this exception.</param>
    public InvalidExpressionException(string? message, Exception innerException)
        : base(message, innerException)
    {
    }

    /// <summary>
    /// Throws <see cref="InvalidExpressionException"/> if expression[position] is unexpected.
    /// </summary>
    /// <param name="expression">Expression you want to check.</param>
    /// <param name="position">Position of the char to be checked.</param>
    /// <param name="expectedChar">The char you expect by position.</param>
    /// <exception cref="InvalidExpressionException">Thrown if expression[position] is unexpected.</exception>
    public static void ThrowIfUnexpectedChar(string expression, int position, char expectedChar)
    {
        if (position >= expression.Length || expression[position] != expectedChar)
        {
            throw new InvalidExpressionException(
                $"Unexpected char in expression at position {position}");
        }
    }
}
