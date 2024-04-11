namespace MapFilterFold.Test;

public class MapFilterFoldTests
{
    [Test]
    public void TestMap()
    {
        var actualMapResult = MapFilterFold.Map(new List<int>() { 1, 2, 3 }, x => x * 2);
        var expectedMapResult = new List<int> { 2, 4, 6 };
        AssertListsForEquality(actualMapResult, expectedMapResult);
    }

    [Test]
    public void TestFilter()
    {
        var actualMapResult = MapFilterFold.Filter(new List<int>() { 1, 2, 3 }, x => x % 2 == 1);
        var expectedMapResult = new List<int> { 1, 3 };
        AssertListsForEquality(actualMapResult, expectedMapResult);
    }

    [Test]
    public void TestFold()
    {
        var actualFoldResult = MapFilterFold.Fold(
            new List<int>() { 1, 2, 3 },
            1,
            (accumulatedValue, currentCollectionElement) => accumulatedValue * currentCollectionElement);
        var expectedFoldResult = 6;
        Assert.That(actualFoldResult, Is.EqualTo(expectedFoldResult));
    }

    private static void AssertListsForEquality(List<int> firstList, List<int> secondList)
    {
        Assert.That(firstList, Has.Count.EqualTo(secondList.Count));
        for (var i = 0; i < firstList.Count; ++i)
        {
            Assert.That(firstList[i], Is.EqualTo(secondList[i]));
        }
    }
}
