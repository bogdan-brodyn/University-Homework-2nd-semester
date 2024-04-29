namespace Kruskal.Tests;

public class TestKruskal
{
    [Test]
    public void TestGetMinimumSpanningTree_WithCorrectInputData_TestCase1()
    {
        var expectedSpanningTree = new List<Edge>
        {
            new (1, 2, 10),
            new (1, 3, 5),
        };
        var edgeList = new EdgeList(
        [
            new (1, 2, 10),
            new (1, 3, 5),
            new (2, 3, 1),
        ]);

        var result = Kruskal.GetMinimumSpanningTree(edgeList);

        CollectionAssert.AreEqual(result, expectedSpanningTree);
    }

    [Test]
    public void TestGetMinimumSpanningTree_WithCorrectInputData_TestCase2()
    {
        var expectedSpanningTree = new List<Edge>
        {
            new (2, 4, 8),
            new (1, 4, 7),
            new (4, 6, 7),
            new (5, 6, 6),
            new (3, 5, 5),
        };
        var edgeList = new EdgeList(
        [
            new (2, 4, 8),
            new (1, 4, 7),
            new (4, 6, 7),
            new (5, 6, 6),
            new (3, 5, 5),
            new (3, 4, 4),
            new (1, 3, 3),
            new (1, 2, 2),
        ]);

        var result = Kruskal.GetMinimumSpanningTree(edgeList);

        CollectionAssert.AreEqual(result, expectedSpanningTree);
    }

    [Test]
    public void TestGetMinimumSpanningTree_WithCorrectInputData_TestCase3()
    {
        var expectedSpanningTree = new List<Edge>
        {
            new (2, 7, 10),
            new (7, 8, 9),
            new (2, 4, 8),
            new (1, 4, 7),
            new (4, 6, 7),
            new (5, 6, 6),
            new (3, 5, 5),
            new (4, 9, 5),
        };
        var edgeList = new EdgeList(
        [
            new (2, 7, 10),
            new (7, 8, 9),
            new (2, 4, 8),
            new (1, 4, 7),
            new (4, 6, 7),
            new (5, 6, 6),
            new (3, 5, 5),
            new (4, 9, 5),
            new (3, 4, 4),
            new (1, 3, 3),
            new (1, 2, 2),
            new (4, 8, 2),
            new (6, 9, 2),
            new (8, 9, 1),
        ]);

        var result = Kruskal.GetMinimumSpanningTree(edgeList);

        CollectionAssert.AreEqual(result, expectedSpanningTree);
    }
}
