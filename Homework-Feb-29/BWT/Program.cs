namespace BWT;

class Program
{
    static void Main(string[] args)
    {
        var tuple = BWT.Transform("ABACABA");
        Console.WriteLine(tuple.Item1);
        Console.WriteLine(tuple.Item2);
    }
}
