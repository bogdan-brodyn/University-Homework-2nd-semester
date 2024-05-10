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

    /// <inheritdoc/>
    public IEnumerator<T> GetEnumerator()
    {
        return new Enumerator(this);
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

    /// <inheritdoc/>
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
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

    private class Enumerator : IEnumerator<T>
    {
        private readonly Skiplist<T> _skiplist;
        private readonly int _version;
        private int _index;
        private SkiplistNode? _current;

        public Enumerator(Skiplist<T> skiplist)
        {
            _skiplist = skiplist;
            _version = skiplist._version;
            _index = -1;
            _current = skiplist._head;
            while (_current.Down is not null)
            {
                _current = _current.Down;
            }
        }

        public T Current
        {
            get
            {
                if (_version != _skiplist._version)
                {
                    throw new InvalidOperationException("Enumerator you use is invalid.");
                }

                if (_index == -1 || _index == _skiplist.Count)
                {
                    throw new InvalidOperationException(
                        "Couldn't get Current because Reset or MoveNext wasn't called previously");
                }

                if (_current is null || _current.Item is null)
                {
                    throw new InvalidOperationException(
                        "Couldn't get Current because data structure integrity has been violated");
                }

                return _current.Item;
            }
        }

        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (_version != _skiplist._version)
            {
                throw new InvalidOperationException("Enumerator you use is invalid.");
            }

            if (_index == _skiplist.Count - 1)
            {
                return false;
            }

            if (_current is null)
            {
                throw new InvalidOperationException(
                    "Couldn't advance next element because data structure integrity has been violated");
            }

            ++_index;
            _current = _current.Next;
            return true;
        }

        public void Reset()
        {
            if (_version != _skiplist._version)
            {
                throw new InvalidOperationException("Enumerator you use is invalid.");
            }

            _index = -1;
            _current = _skiplist._head;
            while (_current.Down is not null)
            {
                _current = _current.Down;
            }
        }
    }
}
