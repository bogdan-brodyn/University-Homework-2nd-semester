using System.Text;
using Kruskal;

if (args.Length != 2)
{
    Console.WriteLine("Application must be run with two arguments.");
    Console.Error.WriteLine("Application must be run with two arguments.");
    Environment.Exit((int)ExitCode.ExitCode.WrongArgumentsCount);
}

var pathToRead = args[0];
var pathToWrite = args[1];
Console.WriteLine($"Application will read graph from file by path: {pathToRead} and write it to file by path: {pathToWrite}");
if (!File.Exists(pathToRead))
{
    Console.WriteLine("File to read from does not exist.");
    Console.Error.WriteLine("File to read from does not exist.");
    Environment.Exit((int)ExitCode.ExitCode.SourceFileNotExist);
}

EdgeList edges;
using (var streamReader = File.OpenText(pathToRead))
{
    edges = new EdgeList(streamReader);
}

var minimumSpanningTree = Kruskal.Kruskal.GetMinimumSpanningTree(edges);
if (minimumSpanningTree is null)
{
    Console.WriteLine("Graph is disconnected.");
    Console.Error.WriteLine("Graph is disconnected.");
    Environment.Exit((int)ExitCode.ExitCode.SourceGraphDisconnected);
}
else
{
    using var streamWriter = new StreamWriter(pathToWrite);
    PrintMinimumSpanningTree(streamWriter, minimumSpanningTree);
    Console.WriteLine($"Application was succesfully executed, you can see the result in file by path: {pathToWrite}");
}

static void PrintMinimumSpanningTree(StreamWriter streamWriter, List<Edge> minimumSpanningTree)
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
