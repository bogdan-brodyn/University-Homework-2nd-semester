namespace MapFilterFold.Test;

public class MapFilterFoldTests
{
    [Test]
    public void TestMap_WithCommonTestCase_ResultMustBeExpected()
    {
        // Arrange
        var inputArray = new int[] { 1, 2, 3 };
        static int MapFunction(int x) => x * 2;

        // Act
        var actualMapResult = MapFilterFold.Map(inputArray, MapFunction);

        // Assert
        var expectedMapResult = new int[] { 2, 4, 6 };
        CollectionAssert.AreEqual(actual: actualMapResult, expected: expectedMapResult);
    }

    [Test]
    public void TestFilter_WithCommonTestCase_ResultMustBeExpected()
    {
        // Arrange
        var inputArray = new int[] { 1, 2, 3 };
        static bool FilterFunction(int x) => x % 2 == 1;

        // Act
        var actualFilterResult = MapFilterFold.Filter(inputArray, FilterFunction);

        // Assert
        var expectedFilterResult = new int[] { 1, 3 };
        CollectionAssert.AreEqual(actual: actualFilterResult, expected: expectedFilterResult);
    }

    [Test]
    public void TestFold_WithCommonTestCase_ResultMustBeExpected()
    {
        // Arrange
        var inputArray = new int[] { 1, 2, 3 };
        var inputAccumulatedValue = 1;
        static int AccumulatorFunction(int accumulatedValue, int currentCollectionElement)
            => accumulatedValue * currentCollectionElement;

        // Act
        var actualFoldResult = MapFilterFold.Fold(inputArray, inputAccumulatedValue, AccumulatorFunction);

        // Assert
        var expectedFoldResult = 6;
        Assert.That(actualFoldResult, Is.EqualTo(expectedFoldResult));
    }
}
