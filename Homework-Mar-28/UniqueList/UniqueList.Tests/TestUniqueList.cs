namespace UniqueList.Tests;

public class TestUniqueList
{
    private UniqueList<int> uniqueList;

    [SetUp]
    public void InitializeUniqueList()
    {
        uniqueList = new ();
    }

    [Test]
    public void Add_WithCorrectInputData()
    {
        int[] elements = { 1, 6, 7, 4, 3, 1242, 335531 };
        for (int i = 0; i < elements.Length; ++i)
        {
            Assert.That(uniqueList.Size, Is.EqualTo(i));
            uniqueList.Add(elements[i]);
        }

        for (int i = 0; i < elements.Length; ++i)
        {
            Assert.That(uniqueList.GetBy(i), Is.EqualTo(elements[i]));
        }
    }

    [Test]
    public void Add_WithIncorrectInputData_ShouldThrowNotUniqueValueException()
    {
        int[] elements = { 1, 235, 124, 4235325, 214, 0, 12 };
        FillUniqueListWithElements(elements);
        foreach (var element in elements)
        {
            Assert.Throws<NotUniqueValueException>(() => uniqueList.Add(element));
        }
    }

    [Test]
    public void Remove_WithCorrectInputData()
    {
        int[] elements = { 8, 1, 2, 6, 9, 1012, 124221, 12412424 };
        FillUniqueListWithElements(elements);
        for (int i = 0; i < elements.Length; ++i)
        {
            uniqueList.Remove();
            Assert.That(uniqueList.Size, Is.EqualTo(elements.Length - i - 1));
        }
    }

    [Test]
    public void ModifyAt_WithCorrectInputData()
    {
        const int elementsCount = 5;
        var elements = new int[elementsCount] { 1, 2, 3, 4, 5 };
        var modifiedElements = new int[elementsCount] { 0, 214535, 2141, 436547, 65567 };

        FillUniqueListWithElements(elements);
        for (int i = 0; i < elementsCount; ++i)
        {
            uniqueList.ModifyAt(modifiedElements[i], i);
        }

        for (int i = 0; i < elementsCount; ++i)
        {
            Assert.That(uniqueList.GetBy(i), Is.EqualTo(modifiedElements[i]));
        }
    }

    [Test]
    public void ModifyAt_WithIncorrectInputData_ShouldThrowNotUniqueValueException()
    {
        int[] elements = { 1, 2, 3, 4, 5 };
        FillUniqueListWithElements(elements);
        for (int i = 1; i < elements.Length; ++i)
        {
            Assert.Throws<NotUniqueValueException>(() => uniqueList.ModifyAt(elements[i - 1], i));
        }
    }

    [Test]
    public void ModifyAt_WithIncorrectInputData_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => uniqueList.ModifyAt(10, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => uniqueList.ModifyAt(10, 0));
        uniqueList.Add(10);
        Assert.Throws<ArgumentOutOfRangeException>(() => uniqueList.ModifyAt(10, 1));
    }

    [Test]
    public void GetBy_WithIncorrectInputData_ShouldThrowArgumentOutOfRangeException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => uniqueList.GetBy(-1));
        Assert.Throws<ArgumentOutOfRangeException>(() => uniqueList.GetBy(0));
        uniqueList.Add(10);
        Assert.Throws<ArgumentOutOfRangeException>(() => uniqueList.GetBy(1));
    }

    private void FillUniqueListWithElements(int[] elements)
    {
        foreach (var element in elements)
        {
            uniqueList.Add(element);
        }
    }
}
