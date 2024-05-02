namespace ConsoleApplication;

using System.Text;
using Kruskal;

public class Program
{
    public static void Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.WriteLine("Application must be run with two arguments.");
            Console.Error.WriteLine("Application must be run with two arguments.");
            Environment.Exit(1);
        }

        var pathToRead = args[0];
        var pathToWrite = args[1];
        Console.WriteLine($"Application will read graph from file by path: {pathToRead} and write it to file by path: {pathToWrite}");
        if (!File.Exists(pathToRead))
        {
            Console.WriteLine("File to read from does not exist.");
            Console.Error.WriteLine("File to read from does not exist.");
            Environment.Exit(1);
        }

        var streamReader = File.OpenText(pathToRead);
        var edges = new EdgeList(streamReader);
        streamReader.Dispose();

        var minimumSpanningTree = Kruskal.GetMinimumSpanningTree(edges);
        if (minimumSpanningTree is null)
        {
            Console.WriteLine("Graph is disconnected.");
            Console.Error.WriteLine("Graph is disconnected.");
            Environment.Exit(1);
        }
        else
        {
            using var streamWriter = new StreamWriter(pathToWrite);
            PrintMinimumSpanningTree(streamWriter, minimumSpanningTree);
            Console.WriteLine($"Application was succesfully executed, you can see the result in file by path: {pathToWrite}");
        }
    }

    private static void PrintMinimumSpanningTree(StreamWriter streamWriter, List<Edge> minimumSpanningTree)
    {
        foreach (var group in minimumSpanningTree
            .OrderBy(x => x.LesserVertex)
            .ThenBy(x => x.GreaterVertex)
            .GroupBy(x => x.LesserVertex))
        {
            var adjacencyListLine = group.Aggregate(
                new StringBuilder(),
                (line, edge) => line.Append($" {edge.GreaterVertex} ({edge.EdgeWeight}),"));
            adjacencyListLine.Remove(adjacencyListLine.Length - 1, 1);
            streamWriter.WriteLine($"{group.Key}: {adjacencyListLine}");
        }
    }
}
