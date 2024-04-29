namespace Kruskal;

/// <summary>
/// Represents graph via edge list sorted by edges weights.
/// </summary>
public class EdgeList
{
    private readonly List<Edge> _edgeList;

    /// <summary>
    /// Initializes a new instance of the <see cref="EdgeList"/> class.
    /// </summary>
    /// <param name="edgeList">List of edges. Will be sorted by weights.</param>
    public EdgeList(List<Edge> edgeList)
    {
        _edgeList = edgeList;
        _edgeList.Sort(Edge.CompareEdgesByWeight);
        foreach (var edge in edgeList)
        {
            VerticesCount = VerticesCount >= edge.GreaterVertex ? VerticesCount : edge.GreaterVertex;
        }
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EdgeList"/> class.
    /// </summary>
    /// <param name="streamReader">Stream to read graph from
    /// (graph should be represented via adjacency list,
    /// vertices must be numbered from 1 to n).</param>
    public EdgeList(StreamReader streamReader)
    {
        _edgeList = new List<Edge>();
        var linesCounter = 1;
        for (
            var currentLine = streamReader.ReadLine();
            currentLine is not null;
            currentLine = streamReader.ReadLine(),
            ++linesCounter)
        {
            try
            {
                ParseAdjacencyListLine(currentLine);
            }
            catch (InvalidGraphRepresentationException e)
            {
                throw new InvalidGraphRepresentationException(
                    $"The line {linesCounter} could not be processed.", e);
            }
        }

        _edgeList.Sort(Edge.CompareEdgesByWeight);
    }

    /// <summary>
    /// Gets <see cref="EdgeList"/> contained edges count.
    /// </summary>
    public int EdgesCount => _edgeList.Count;

    /// <summary>
    /// Gets <see cref="EdgeList"/> contained vertices count.
    /// </summary>
    public int VerticesCount { get; private set; }

    /// <summary>
    /// Gets edge by index.
    /// </summary>
    /// <param name="index">Index of edge you want to get.</param>
    /// <returns>Edge in <see cref="EdgeList"/> by index.</returns>
    public Edge GetEdge(int index) => _edgeList[index];

    private static int GetNumberFromLine(string line, int startPosition, out int endPosition)
    {
        var separators = new char[] { ' ', ')', ':' };
        endPosition = line.IndexOfAny(separators, startPosition);
        if (endPosition == -1)
        {
            throw new InvalidGraphRepresentationException("Invalid line ending.");
        }

        var wasConvertedSuccessfully = int.TryParse(
            line.AsSpan(
                startPosition,
                endPosition - startPosition + 1),
            out int conversionResult);
        if (!wasConvertedSuccessfully)
        {
            throw new InvalidGraphRepresentationException("The number could not be processed.");
        }

        return conversionResult;
    }

    private void ParseAdjacencyListLine(string adjacencyListLine)
    {
        var currentPosition = 0;
        var lesserVertex = GetNumberFromLine(adjacencyListLine, currentPosition, out currentPosition);
        InvalidGraphRepresentationException.ThrowIfUnexpectedChar(adjacencyListLine, currentPosition++, ':');
        while (true)
        {
            InvalidGraphRepresentationException.ThrowIfUnexpectedChar(adjacencyListLine, currentPosition++, ' ');
            var greaterVertex = GetNumberFromLine(adjacencyListLine, currentPosition, out currentPosition);
            InvalidGraphRepresentationException.ThrowIfUnexpectedChar(adjacencyListLine, currentPosition++, ' ');
            VerticesCount = VerticesCount >= greaterVertex ? VerticesCount : greaterVertex;

            InvalidGraphRepresentationException.ThrowIfUnexpectedChar(adjacencyListLine, currentPosition++, '(');
            var edgeWeight = GetNumberFromLine(adjacencyListLine, currentPosition, out currentPosition);
            InvalidGraphRepresentationException.ThrowIfUnexpectedChar(adjacencyListLine, currentPosition++, ')');

            _edgeList.Add(new Edge(lesserVertex, greaterVertex, edgeWeight));

            if (currentPosition == adjacencyListLine.Length)
            {
                break;
            }

            InvalidGraphRepresentationException.ThrowIfUnexpectedChar(adjacencyListLine, currentPosition++, ',');
        }
    }
}
