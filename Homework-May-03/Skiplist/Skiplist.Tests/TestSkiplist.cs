namespace Skiplist.Tests;

using Skiplist;

public class TestSkiplist
{
    [Test]
    public void TestAdd()
    {
        // Arrange
        var skiplist = new Skiplist<int>();
        var itemsContained = new int[] { 0, 1, 3, 3, 10, 2, 3, -100, 15, 12 };
        var itemsNotContained = new int[] { 4, 5, -15, 4, -20, 1000, 20, 4 };

        // Act
        foreach (var item in itemsContained)
        {
            skiplist.Add(item);
        }

        // Assert
        foreach (var item in itemsContained)
        {
            Assert.That(skiplist.Contains(item), Is.True);
        }

        foreach (var item in itemsNotContained)
        {
            Assert.That(skiplist.Contains(item), Is.False);
        }
    }

    [Test]
    public void TestRemove()
    {
        // Arrange
        var skiplist = new Skiplist<int>();
        var itemsContained = new int[] { 0, 1, 3, 3, 10, 2, 3, -100, 15, 12, 1, 0 };
        var itemsToRemove = new int[] { 0, 1, 3, 10, 3, -100, 15, 3 };
        var itemsStillContained = new int[] { 0, 1, 2, 12 };
        var itemsNotContained = new int[] { 3, 10, -100, 15 };
        foreach (var item in itemsContained)
        {
            skiplist.Add(item);
        }

        // Act
        foreach (var item in itemsToRemove)
        {
            Assert.That(skiplist.Remove(item), Is.True);
        }

        // Assert
        foreach (var item in itemsStillContained)
        {
            Assert.That(skiplist.Contains(item), Is.True);
        }

        foreach (var item in itemsNotContained)
        {
            Assert.That(skiplist.Contains(item), Is.False);
        }
    }
}
