namespace StackCalculator;

using System.Text;

/// <summary>
/// Implements the execution of operations +, -, *, / on an arithmetic expression as a string in a postfix entry.
/// </summary>
public static class StackCalculator
{
    private static readonly Dictionary<string, Func<double, double, double>> Operations = new ()
    {
        { "+", (x, y) => x + y },
        { "-", (x, y) => x - y },
        { "*", (x, y) => x * y },
        {
            "/", (x, y) =>
            {
                if (Math.Abs(y) < 1e-10)
                {
                    throw new DivideByZeroException();
                }

                return x / y;
            }
        },
    };

    /// <summary>
    /// Сalculates an arithmetic expression as a string in a postfix entry.
    /// </summary>
    /// <param name="expression">String in a postfix entry.</param>
    /// <returns>Сalculation result.</returns>
    /// <exception cref="InvalidOperationException">Invalid expression.</exception>
    /// <exception cref="DivideByZeroException">You are trying to divide by zero.</exception>
    public static double Calculate(string expression)
    {
        if (expression == string.Empty)
        {
            throw new InvalidOperationException("Expression is empty string");
        }

        var stack = new Stack<double>();
        var expressionElement = new StringBuilder();
        for (var i = 0; i < expression.Length; ++i)
        {
            if (expression[i] != ' ')
            {
                expressionElement.Append(expression[i]);
            }

            if (expression[i] == ' ' || i == expression.Length - 1)
            {
                try
                {
                    Process(expressionElement.ToString(), stack);
                }
                catch (DivideByZeroException exception)
                {
                    throw new DivideByZeroException(
                        "Invalid expression: You are trying to divide by zero",
                        exception);
                }
                catch (InvalidOperationException exception)
                {
                    throw new InvalidOperationException(
                        "Invalid expression: Amount of operations does not match amount of numbers",
                        exception);
                }
                catch (Exception exception)
                {
                    throw new InvalidOperationException(
                        $"Invalid expression: Pay attention to the indexes from {i - expression.Length} to {i}",
                        exception);
                }

                expressionElement.Clear();
            }
        }

        if (stack.Count != 1)
        {
            throw new InvalidOperationException(
                "Invalid expression: Amount of operations does not match amount of numbers");
        }

        return stack.Pop();
    }

    private static void Process(string nextExpressionElement, Stack<double> stack)
    {
        bool isOperation = Operations.TryGetValue(nextExpressionElement, out var operation);
        if (isOperation && operation is not null)
        {
            var y = stack.Pop();
            var x = stack.Pop();
            var operationResult = operation(x, y);
            stack.Push(operationResult);
        }
        else
        {
            stack.Push(int.Parse(nextExpressionElement));
        }
    }
}
