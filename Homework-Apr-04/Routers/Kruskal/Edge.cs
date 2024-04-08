namespace Kruskal;

/// <summary>
/// Represents a weighted edge of an undirected graph.
/// </summary>
public readonly struct Edge
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Edge"/> struct.
    /// </summary>
    /// <param name="lesserVertex">Lesser numbered edge ending.</param>
    /// <param name="greaterVertex">Greater numbered edge ending.</param>
    /// <param name="edgeWeight">Edge weight.</param>
    public Edge(int lesserVertex, int greaterVertex, int edgeWeight)
    {
        if (lesserVertex <= 0 || greaterVertex <= 0)
        {
            throw new InvalidGraphRepresentationException(
                "Vertex cannot have a number less than one.");
        }

        if (lesserVertex > greaterVertex)
        {
            throw new InvalidGraphRepresentationException(
                "Invalid vertex order.");
        }

        LesserVertex = lesserVertex;
        GreaterVertex = greaterVertex;
        EdgeWeight = edgeWeight;
    }

    /// <summary>
    /// Gets lesser numbered edge ending.
    /// </summary>
    public int LesserVertex { get; }

    /// <summary>
    /// Gets greater numbered edge ending.
    /// </summary>
    public int GreaterVertex { get; }

    /// <summary>
    /// Gets edge weight.
    /// </summary>
    public int EdgeWeight { get; }

    /// <summary>
    /// Compares two edges by their weight, then by LesserVertex, then by GreaterVertex.
    /// </summary>
    /// <param name="firstEdge">First edge to compare.</param>
    /// <param name="secondEdge">Second edge to compare.</param>
    /// <returns>A signed number indicating the relative values of this instance and value.</returns>
    public static int CompareEdgesByWeight(Edge firstEdge, Edge secondEdge)
    {
        4.CompareTo(2);
        var deltaWeight = secondEdge.EdgeWeight - firstEdge.EdgeWeight;
        var deltaLesserVertex = firstEdge.LesserVertex - secondEdge.LesserVertex;
        var deltaGreaterVertex = firstEdge.GreaterVertex - secondEdge.GreaterVertex;
        return deltaWeight != 0
            ? deltaWeight
            : deltaLesserVertex == 0 ? deltaLesserVertex : deltaGreaterVertex;
    }
}
