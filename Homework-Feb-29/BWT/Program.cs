namespace BWT;

class Program
{
    static bool Test()
    {
        var testCase = new List<string> {"", "A", "ABC", "AAABBC", 
                                            "1234567890", "ABACABA"};
        for (int i = 0; i < testCase.Count; ++i)
        {
            if (testCase[i] != BWT.ReverseTransform(BWT.Transform(testCase[i])))
            {
                return false;
            }
        }
        return true;
    }

    static void Main(string[] args)
    {
        Console.WriteLine(Test() ? 
                "Program has passed all the tests" : "Program failed test");
    }
}
