namespace Trie.Tests;

/// <summary>
/// Tests for Trie data structure.
/// </summary>
public class TestTrie
{
    /// <summary>
    /// Checks if Add and Contains methods work correctly.
    /// </summary>
    [Test]
    public void CheckAddAndContainsMethods()
    {
        var trie = new Trie();
        Assert.Multiple(() =>
        {
            // Add the first element
            Assert.That(trie.Add("a"), Is.True);
            Assert.That(trie.Contains("a"), Is.True);
            Assert.That(trie.Add("a"), Is.False);
            Assert.That(trie.Contains("b"), Is.False);
            Assert.That(trie.Contains("aa"), Is.False);

            // Add the second element and check the first
            Assert.That(trie.Add("b"), Is.True);
            Assert.That(trie.Contains("a"), Is.True);
            Assert.That(trie.Contains("b"), Is.True);
            Assert.That(trie.Contains("aa"), Is.False);

            // Add more elements and check contain method
            Assert.That(trie.Add("aaba"), Is.True);
            Assert.That(trie.Add("aabb"), Is.True);
            Assert.That(trie.Add("abaa"), Is.True);
            Assert.That(trie.Contains("aaba"), Is.True);
            Assert.That(trie.Contains("aabb"), Is.True);
            Assert.That(trie.Contains("abaa"), Is.True);
            Assert.That(trie.Contains("aa"), Is.False);
        });
    }

    /// <summary>
    /// Checks if Remove method works correctly.
    /// </summary>
    [Test]
    public void CheckRemoveMethod()
    {
        var trie = new Trie();
        Assert.Multiple(() =>
        {
            // Add start elements
            Assert.That(trie.Add("a"), Is.True);
            Assert.That(trie.Add("aaba"), Is.True);
            Assert.That(trie.Add("aabb"), Is.True);
            Assert.That(trie.Add("abaa"), Is.True);

            // Check remove method with the first element
            Assert.That(trie.Remove("a"), Is.True);
            Assert.That(trie.Contains("a"), Is.False);
            Assert.That(trie.Remove("a"), Is.False);
            Assert.That(trie.Contains("aaba"), Is.True);
            Assert.That(trie.Contains("aabb"), Is.True);
            Assert.That(trie.Contains("abaa"), Is.True);

            // Check remove method with the second element
            Assert.That(trie.Remove("aaba"), Is.True);
            Assert.That(trie.Contains("aaba"), Is.False);
            Assert.That(trie.Remove("aaba"), Is.False);
            Assert.That(trie.Remove("a"), Is.False);
            Assert.That(trie.Contains("aabb"), Is.True);
            Assert.That(trie.Contains("abaa"), Is.True);
        });
    }

    /// <summary>
    /// Checks if HowManyStartsWithPrefix method works correctly.
    /// </summary>
    [Test]
    public void CheckHowManyStartsWithPrefixMethod()
    {
        var trie = new Trie();
        Assert.Multiple(() =>
        {
            Assert.That(trie.HowManyStartsWithPrefix("a"), Is.EqualTo(0));
            Assert.That(trie.Add("a"), Is.True);
            Assert.That(trie.HowManyStartsWithPrefix("a"), Is.EqualTo(1));
            Assert.That(trie.Add("aaba"), Is.True);
            Assert.That(trie.Add("aabb"), Is.True);
            Assert.That(trie.Add("abaa"), Is.True);
            Assert.That(trie.HowManyStartsWithPrefix("a"), Is.EqualTo(4));
            Assert.That(trie.Remove("aabb"), Is.True);
            Assert.That(trie.HowManyStartsWithPrefix("a"), Is.EqualTo(3));
        });
    }

    /// <summary>
    /// Checks if Trie data structure methods throw argument exception.
    /// </summary>
    [Test]
    public void CheckArgumentExceptionThrown()
    {
        var trie = new Trie();
        Assert.Throws<ArgumentException>(() => trie.Add(string.Empty));
        Assert.Throws<ArgumentException>(() => trie.Contains(string.Empty));
        Assert.Throws<ArgumentException>(() => trie.Remove(string.Empty));
        Assert.Throws<ArgumentException>(() => trie.HowManyStartsWithPrefix(string.Empty));
    }
}