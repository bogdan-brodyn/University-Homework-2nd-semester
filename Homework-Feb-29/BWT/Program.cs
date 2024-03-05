namespace BWT;

public class Program
{
    public static bool Test()
    {
        var testCase = new List<string> {"", "A", "ABC", "AAABBC", 
                                            "1234567890", "ABACABA"};
        for (int i = 0; i < testCase.Count; ++i)
        {
            var (transformedString, sourceStringPosition) = BWT.Transform(testCase[i]);
            if (testCase[i] != BWT.ReverseTransform(transformedString, sourceStringPosition))
            {
                return false;
            }
        }
        return true;
    }

    public static void Main(string[] args)
    {
        Console.WriteLine(Test() ? 
                "Program has passed all the tests" : "Program failed test");
    }
}
