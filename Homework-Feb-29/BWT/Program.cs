namespace BWT;

public class Program
{
    private static bool RunTestCase(
        string testCase, 
        string correctTransformedString, 
        int correctSourceStringPosition)
    {
        var (transformedString, sourceStringPosition) = BWT.Transform(testCase);
        return correctTransformedString == transformedString
                && correctSourceStringPosition == sourceStringPosition
                && testCase == BWT.ReverseTransform(transformedString, sourceStringPosition);
    }

    private static (int, int) RunTests()
    {
        var testCases = new List<string> {"", "A", "ABC", "AAABBC", "1234567890", "ABACABA"};
        var correctTransformedStrings = new List<string> 
                                        {"", "A", "CAB", "CAAABB", "9012345678", "BCABAAA"};
        var correctSourceStringPositions = new List<int> {0, 0, 0, 0, 1, 2};
        for (int i = 0; i < testCases.Count; ++i)
        {
            var isTestCasePassed = RunTestCase(testCases[i], 
                                                correctTransformedStrings[i], 
                                                correctSourceStringPositions[i]);
            if (!isTestCasePassed)
            {
                return (i, testCases.Count);
            }
        }
        return (testCases.Count, testCases.Count);
    }

    public static void Main(string[] args)
    {
        var (passedTestsCount, testsCount) = RunTests();
        Console.WriteLine($"Program has passed {passedTestsCount}/{testsCount} tests");
    }
}
