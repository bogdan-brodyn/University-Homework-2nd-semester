namespace BWT;

class Program
{
    static void Main(string[] args)
    {
        var tuple = BWT.Transform("ABACABA");
        Console.WriteLine(tuple.Item1);
        Console.WriteLine(tuple.Item2);
        Console.WriteLine(BWT.ReverseTransform(BWT.Transform("ABACABA")));
        Console.WriteLine(BWT.ReverseTransform(BWT.Transform("ABC")));
        Console.WriteLine(BWT.ReverseTransform(BWT.Transform("A")));
        Console.WriteLine(BWT.ReverseTransform(BWT.Transform("")));
    }
}
