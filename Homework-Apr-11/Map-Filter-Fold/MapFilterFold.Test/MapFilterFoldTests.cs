namespace MapFilterFold.Test;

public class MapFilterFoldTests
{
    [Test]
    public void TestMap()
    {
        var inputArray = new int[] { 1, 2, 3 };
        static int MapFunction(int x) => x * 2;
        var expectedMapResult = new int[] { 2, 4, 6 };

        var actualMapResult = MapFilterFold.Map(inputArray, MapFunction);

        CollectionAssert.AreEqual(actualMapResult, expectedMapResult);
    }

    [Test]
    public void TestFilter()
    {
        var inputArray = new int[] { 1, 2, 3 };
        static bool FilterFunction(int x) => x % 2 == 1;
        var expectedFilterResult = new int[] { 1, 3 };

        var actualFilterResult = MapFilterFold.Filter(inputArray, FilterFunction);

        CollectionAssert.AreEqual(actualFilterResult, expectedFilterResult);
    }

    [Test]
    public void TestFold()
    {
        var inputArray = new int[] { 1, 2, 3 };
        var inputAccumulatedValue = 1;
        static int AccumulatorFunction(int accumulatedValue, int currentCollectionElement)
            => accumulatedValue * currentCollectionElement;
        var expectedFoldResult = 6;

        var actualFoldResult = MapFilterFold.Fold(inputArray, inputAccumulatedValue, AccumulatorFunction);

        Assert.That(actualFoldResult, Is.EqualTo(expectedFoldResult));
    }
}
