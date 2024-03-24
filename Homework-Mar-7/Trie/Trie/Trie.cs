namespace Trie;

/// <summary>
/// Trie data structure implementation.
/// </summary>
public class Trie
{
    private readonly Vertex root = new ();

    /// <summary>
    /// Gets elements count.
    /// </summary>
    public int Size => this.root.Count;

    /// <summary>
    /// Check if the element is contained.
    /// </summary>
    /// <param name="element">The element to check.</param>
    /// <returns>True if the element is contained.</returns>
    /// <exception cref="ArgumentException">Thrown if the argument is an empty string.</exception>
    public bool Contains(string element)
    {
        ArgumentException.ThrowIfNullOrEmpty(element);

        var currentVertex = this.root;
        foreach (var currentChar in element)
        {
            var isContained = currentVertex.Next.TryGetValue(currentChar, out var nextVertex);
            if (!isContained || nextVertex is null)
            {
                return false;
            }

            currentVertex = nextVertex;
        }

        return currentVertex.IsTerminal;
    }

    /// <summary>
    /// Add the element to data structure.
    /// </summary>
    /// <param name="element">Element to add.</param>
    /// <returns>True if the element was not previously contained.</returns>
    /// <exception cref="ArgumentException">Thrown if the argument is an empty string.</exception>
    public bool Add(string element)
    {
        ArgumentException.ThrowIfNullOrEmpty(element);

        if (this.Contains(element))
        {
            return false;
        }

        this.root.Count++;
        var currentVertex = this.root;
        foreach (var currentChar in element)
        {
            var isContained = currentVertex.Next.TryGetValue(currentChar, out var nextVertex);
            if (!isContained || nextVertex is null)
            {
                nextVertex = new Vertex();
                currentVertex.Next.Add(currentChar, nextVertex);
            }

            currentVertex = nextVertex;
            currentVertex.Count++;
        }

        currentVertex.IsTerminal = true;
        return true;
    }

    /// <summary>
    /// Get count of elements starting with the prefix.
    /// </summary>
    /// <param name="prefix">The prefix.</param>
    /// <returns>Count of elements starting with the prefix.</returns>
    /// <exception cref="ArgumentException">Thrown if the argument is an empty string.</exception>
    public int HowManyStartsWithPrefix(string prefix)
    {
        ArgumentException.ThrowIfNullOrEmpty(prefix);

        var currentVertex = this.root;
        foreach (var currentChar in prefix)
        {
            var isContained = currentVertex.Next.TryGetValue(currentChar, out var nextVertex);
            if (!isContained || nextVertex is null)
            {
                return 0;
            }

            currentVertex = nextVertex;
        }

        return currentVertex.Count;
    }

    /// <summary>
    /// Remove the element from data structure.
    /// </summary>
    /// <param name="element">The element to remove.</param>
    /// <returns>True if the element was contained.</returns>
    /// <exception cref="ArgumentException">Thrown if the argument is an empty string.</exception>
    public bool Remove(string element)
    {
        ArgumentException.ThrowIfNullOrEmpty(element);

        if (!this.Contains(element))
        {
            return false;
        }

        this.root.Count--;
        var currentVertex = this.root;
        foreach (var currentChar in element)
        {
            var nextVertex = currentVertex.Next[currentChar];
            if (nextVertex.Count == 1)
            {
                currentVertex.Next.Remove(currentChar);
                return true;
            }

            currentVertex = nextVertex;
            currentVertex.Count--;
        }

        currentVertex.IsTerminal = false;
        return true;
    }

    private class Vertex
    {
        public Dictionary<char, Vertex> Next { get; } = new ();

        public bool IsTerminal { get; set; } = false;

        public int Count { get; set; } = 0;
    }
}
