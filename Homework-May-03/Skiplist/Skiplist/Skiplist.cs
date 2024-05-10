namespace Skiplist;

using System.Collections;

public class Skiplist<T> : IList<T>
    where T : IComparable<T>
{
    private int _count = 0;
    private int _version = 0;
    private SkiplistNode _head = new ();

    /// <inheritdoc/>
    public int Count => _count;

    /// <inheritdoc/>
    public bool IsReadOnly => false;

    public T this[int index] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    /// <inheritdoc/>
    public void Add(T item)
    {
        ++_version;
        ++_count;
        var nodeToBeDuplicated = Add(item, _head);
        if (nodeToBeDuplicated is not null)
        {
            var newLevelFirstNode = new SkiplistNode(item: item, next: null, down: nodeToBeDuplicated);
            _head = new SkiplistNode(next: newLevelFirstNode, down: _head);
        }
    }

    /// <inheritdoc/>
    public void Clear()
    {
        _head = new SkiplistNode();
    }

    /// <inheritdoc/>
    public bool Contains(T item) => Contains(item, _head);

    public void CopyTo(T[] array, int arrayIndex)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public int IndexOf(T item)
    {
        throw new NotImplementedException();
    }

    /// <inheritdoc/>
    public void Insert(int index, T item)
    {
        throw new NotSupportedException();
    }

    /// <inheritdoc/>
    public bool Remove(T item)
    {
        ++_version;
        --_count;
        return Remove(item, _head) is not null;
    }

    public void RemoveAt(int index)
    {
        throw new NotImplementedException();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        throw new NotImplementedException();
    }

    private static bool FlipFairCoin()
    {
        return Random.Shared.NextDouble() < 0.5;
    }

    private static SkiplistNode? Add(T item, SkiplistNode node)
    {
        while (node.Next is not null
            && node.Next.Item!.CompareTo(item) <= 0)
        {
            node = node.Next;
        }

        if (node.Down is null)
        {
            node.Next = new SkiplistNode(item: item, next: node.Next, down: null);
            return FlipFairCoin() ? node.Next : null;
        }

        var nodeToBeDuplicated = Add(item, node.Down);
        if (nodeToBeDuplicated is null)
        {
            return null;
        }

        node.Next = new SkiplistNode(item: item, next: node.Next, down: nodeToBeDuplicated);
        return FlipFairCoin() ? node.Next : null;
    }

    private static bool Contains(T item, SkiplistNode node)
    {
        while (node.Next is not null
            && node.Next.Item!.CompareTo(item) < 0)
        {
            node = node.Next;
        }

        return (node.Next is not null && node.Next.Item!.CompareTo(item) == 0)
            || (node.Down is not null && Contains(item, node.Down));
    }

    private static SkiplistNode? Remove(T item, SkiplistNode node)
    {
        while (node.Next is not null
            && node.Next.Item!.CompareTo(item) < 0)
        {
            node = node.Next;
        }

        if (node.Down is null)
        {
            if (node.Next is not null
                && node.Next.Item!.CompareTo(item) == 0)
            {
                var nodeToRemove = node.Next;
                node.Next = node.Next.Next;
                return nodeToRemove;
            }

            return null;
        }

        var removedNode = Remove(item, node.Down);
        if (node.Next is not null
            && ReferenceEquals(node.Next.Down, removedNode))
        {
            removedNode = node.Next;
            node.Next = node.Next.Next;
        }

        return removedNode;
    }

    private class SkiplistNode
    {
        internal SkiplistNode()
        {
        }

        internal SkiplistNode(SkiplistNode? next, SkiplistNode? down)
        {
            Next = next;
            Down = down;
        }

        internal SkiplistNode(T item, SkiplistNode? next, SkiplistNode? down)
        {
            Item = item;
            Next = next;
            Down = down;
        }

        internal T? Item { get; }

        internal SkiplistNode? Next { get; set; }

        internal SkiplistNode? Down { get; set; }
    }
}
