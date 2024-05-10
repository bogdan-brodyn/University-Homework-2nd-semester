namespace Skiplist.Tests;

using Skiplist;

public class TestSkiplist
{
    [Test]
    public void TestAddMethodInfluenceToDataStructure_ConsequencesMustBeExpected()
    {
        // Arrange
        var skiplist = new Skiplist<int>();
        var itemsToAdd = new int[] { 0, 1, 3, 3, 10, 2, 3, -100, 15, 12 };

        // Act
        foreach (var item in itemsToAdd)
        {
            skiplist.Add(item);
        }

        // Assert
        var itemsExpectedToBeContained = itemsToAdd;
        var itemsExpectedNotToBeContained = new int[] { 4, 5, -15, 4, -20, 1000, 20, 4 };
        foreach (var item in itemsExpectedToBeContained)
        {
            Assert.That(skiplist.Contains(item), Is.True);
        }

        foreach (var item in itemsExpectedNotToBeContained)
        {
            Assert.That(skiplist.Contains(item), Is.False);
        }
    }

    [Test]
    public void TestRemoveMethodInfluenceToDataStructure_ConsequencesMustBeExpected()
    {
        // Arrange
        var skiplist = new Skiplist<int>();
        var itemsToAdd = new int[] { 0, 1, 3, 3, 10, 2, 3, -100, 15, 12, 1, 0 };
        var itemsToRemove = new int[] { 0, 1, 3, 10, 3, -100, 15, 3 };

        // Act
        foreach (var item in itemsToAdd)
        {
            skiplist.Add(item);
        }

        foreach (var item in itemsToRemove)
        {
            skiplist.Remove(item);
        }

        // Assert
        var itemsExpectedToBeContained = new int[] { 0, 1, 2, 12 };
        var itemsExpectedNotToBeContained = new int[] { 3, 10, -100, 15 };
        foreach (var item in itemsExpectedToBeContained)
        {
            Assert.That(skiplist.Contains(item), Is.True);
        }

        foreach (var item in itemsExpectedNotToBeContained)
        {
            Assert.That(skiplist.Contains(item), Is.False);
        }
    }

    [Test]
    public void TestRemoveMethodReturnValue_MustBeExpected()
    {
        // Arrange
        var skiplist = new Skiplist<int>();
        var itemToAddThenRemoveTwice = 0;

        // Act
        skiplist.Add(itemToAddThenRemoveTwice);
        var actualFirstRemoveResult = skiplist.Remove(itemToAddThenRemoveTwice);
        var actualSecondRemoveResult = skiplist.Remove(itemToAddThenRemoveTwice);

        // Assert
        Assert.That(actualFirstRemoveResult, Is.True);
        Assert.That(actualSecondRemoveResult, Is.False);
    }

    [Test]
    public void TestEnumeratorUsingCommonCase()
    {
        // Arrange
        var skiplist = new Skiplist<int>();
        var itemsToAdd = new int[] { -12, -5, 0, 2, 5, 7, 10 };

        // Act
        foreach (var item in itemsToAdd)
        {
            skiplist.Add(item);
        }

        // Assert
        CollectionAssert.AreEqual(expected: itemsToAdd, actual: skiplist);
    }

    [Test]
    public void TestEnumeratorInvalidationWhenCollectionChanges()
    {
        // Arrange
        var skiplist = new Skiplist<int>();

        // Act
        var enumerator = skiplist.GetEnumerator();
        skiplist.Add(0);

        // Assert
        Assert.Throws<InvalidOperationException>(() => enumerator.MoveNext());
        Assert.Throws<InvalidOperationException>(enumerator.Reset);
    }
}
