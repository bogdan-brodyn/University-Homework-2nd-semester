namespace ParseTree;

#pragma warning disable SA1600 // Elements should be documented
public class InvalidExpressionException : Exception
{
    public InvalidExpressionException()
    {
    }

    public InvalidExpressionException(string? message)
        : base(message)
    {
    }

    public InvalidExpressionException(string? message, Exception innerException)
        : base(message, innerException)
    {
    }

    public static void ThrowIfUnexpectedChar(string expression, int position, char expectedChar)
    {
        if (position >= expression.Length || expression[position] != expectedChar)
        {
            throw new InvalidExpressionException(
                $"Unexpected char in expression at position {position}");
        }
    }
}
#pragma warning restore SA1600 // Elements should be documented
