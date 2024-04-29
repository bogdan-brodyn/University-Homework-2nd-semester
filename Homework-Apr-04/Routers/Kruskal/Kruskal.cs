namespace Kruskal;

/// <summary>
/// Implementation of Kruskul algorithm for searching minimum spanning tree.
/// </summary>
public static class Kruskal
{
    /// <summary>
    /// Kruskul algorithm for searching minimum spanning tree.
    /// </summary>
    /// <param name="edgeList">Edges' list representation of the graph whose minimum spanning tree you need.</param>
    /// <returns>Minimum spanning tree if it could be build, otherwise null.</returns>
    public static List<Edge>? GetMinimumSpanningTree(EdgeList edgeList)
    {
        var (sizes, parents) = Initialize(edgeList);
        var minOstov = new List<Edge>();
        for (var i = 0; i < edgeList.EdgesCount; ++i)
        {
            Edge currentEdge = edgeList.GetEdge(i);
            if (GetVertexConnectivityComponent(sizes, parents, currentEdge.LesserVertex - 1)
                != GetVertexConnectivityComponent(sizes, parents, currentEdge.GreaterVertex - 1))
            {
                minOstov.Add(currentEdge);
                UniteVerticesConnectivityComponents(
                    sizes,
                    parents,
                    currentEdge.LesserVertex - 1,
                    currentEdge.GreaterVertex - 1);
            }
        }

        return minOstov.Count == edgeList.VerticesCount - 1 ? minOstov : null;
    }

    private static (int[] Sizes, int[] Parents) Initialize(EdgeList edgeList)
    {
        var sizes = new int[edgeList.VerticesCount];
        var parents = new int[edgeList.VerticesCount];
        for (var i = 0; i < sizes.Length; ++i)
        {
            sizes[i] = 1;
            parents[i] = i;
        }

        return (sizes, parents);
    }

    private static int GetVertexConnectivityComponent(int[] sizes, int[] parents, int vertex)
    {
        if (parents[vertex] == vertex)
        {
            return vertex;
        }

        return parents[vertex] = GetVertexConnectivityComponent(sizes, parents, parents[vertex]);
    }

    private static void UniteVerticesConnectivityComponents(
        int[] sizes, int[] parents, int firstVertex, int secondVertex)
    {
        firstVertex = GetVertexConnectivityComponent(sizes, parents, firstVertex);
        secondVertex = GetVertexConnectivityComponent(sizes, parents, secondVertex);
        if (firstVertex == secondVertex)
        {
            return;
        }

        if (sizes[firstVertex] < sizes[secondVertex])
        {
            parents[firstVertex] = secondVertex;
            sizes[secondVertex] += sizes[firstVertex];
        }
        else
        {
            parents[secondVertex] = firstVertex;
            sizes[firstVertex] += sizes[secondVertex];
        }
    }
}
