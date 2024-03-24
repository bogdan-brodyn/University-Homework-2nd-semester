namespace Trie.Tests;

public class TestTrie
{
    private Trie trie;

    [SetUp]
    public void InitializeTrie()
    {
        trie = new ();
    }

    [Test]
    public void Add_WithCorrectInputData_ShouldReturnTrueIfElementIsNew()
    {
        string[] elements = { "a", "b", "aaba", "aabb", "abaa", "a12", "a1" };
        for (var i = 0; i < elements.Length; ++i)
        {
            Assert.That(trie.Add(elements[i]), Is.True);
            Assert.That(trie.Add(elements[i]), Is.False);
            Assert.That(trie.Size, Is.EqualTo(i + 1));
            for (var j = 0; j < elements.Length; ++j)
            {
                Assert.That(trie.Contains(elements[j]), j <= i ? Is.True : Is.False);
            }
        }
    }

    [Test]
    public void Remove_WithCorrectInputData_ShouldReturnTrueIfElementWasContained()
    {
        string[] elements = { "u", "h", "uh", "uuuu", "uhuhk23", "hhuk1dgd" };
        foreach (var element in elements)
        {
            trie.Add(element);
        }

        for (var i = 0; i < elements.Length; ++i)
        {
            Assert.That(trie.Remove(elements[i]), Is.True);
            Assert.That(trie.Remove(elements[i]), Is.False);
            Assert.That(trie.Size, Is.EqualTo(elements.Length - (i + 1)));
            for (var j = 0; j < elements.Length; ++j)
            {
                Assert.That(trie.Contains(elements[j]), j <= i ? Is.False : Is.True);
            }
        }
    }

    [Test]
    public void CheckHowManyStartsWithPrefix_WithCorrectInputData_ShouldReturnCount()
    {
        Assert.That(trie.HowManyStartsWithPrefix("a"), Is.EqualTo(0));
        trie.Add("a");
        Assert.That(trie.HowManyStartsWithPrefix("a"), Is.EqualTo(1));
        trie.Add("aaba");
        trie.Add("aabb");
        trie.Add("abaa");
        Assert.That(trie.HowManyStartsWithPrefix("a"), Is.EqualTo(4));
        trie.Remove("aabb");
        Assert.That(trie.HowManyStartsWithPrefix("a"), Is.EqualTo(3));
    }

    [Test]
    public void Add_WithIncorrectInputData_ShouldThrowArgumentException() =>
        Assert.Throws<ArgumentException>(() => trie.Add(string.Empty));

    [Test]
    public void Contains_WithIncorrectInputData_ShouldThrowArgumentException() =>
        Assert.Throws<ArgumentException>(() => trie.Contains(string.Empty));

    [Test]
    public void Remove_WithIncorrectInputData_ShouldThrowArgumentException() =>
        Assert.Throws<ArgumentException>(() => trie.Remove(string.Empty));

    [Test]
    public void HowManyStartsWithPrefix_WithIncorrectInputData_ShouldThrowArgumentException() =>
        Assert.Throws<ArgumentException>(() => trie.HowManyStartsWithPrefix(string.Empty));
}
