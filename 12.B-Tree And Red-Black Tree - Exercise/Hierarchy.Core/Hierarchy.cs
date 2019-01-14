
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class Hierarchy<T> : IHierarchy<T>
{
    private Node root;
    private Dictionary<T, Node> dictionary;

    private class Node
    {
        public Node(T value, Node parent = null)
        {
            this.Value = value;
            this.Children = new List<Node>();
            this.Parent = parent;
        }

        public T Value { get; set; }

        public List<Node> Children { get; private set; }

        public Node Parent { get; set; }
    }

    public Hierarchy(T root)
    {
        this.root = new Node(root);
        this.dictionary = new Dictionary<T, Node>();
        dictionary.Add(this.root.Value, this.root);
    }

    public int Count
    {
        get
        {
            return this.dictionary.Count;
        }
    }

    public void Add(T element, T child)
    {
        if (!this.dictionary.ContainsKey(element))
        {
            throw new ArgumentException("Element does not exists");
        }

        if (this.dictionary.ContainsKey(child))
        {
            throw new ArgumentException("Child already exists");
        }

        var current = this.dictionary[element];
        var newNode = new Node(child, current);
        current.Children.Add(newNode);
        this.dictionary[newNode.Value] = newNode;
    }

    public void Remove(T element)
    {
        if (!this.dictionary.ContainsKey(element))
        {
            throw new ArgumentException("Element does not exist!");
        }

        var node = this.dictionary[element];

        if (node.Parent == null)
        {
            throw new InvalidOperationException("Can not Delete root node!");
        }

        var parent = node.Parent;
        parent.Children.Remove(node);

        foreach (var ch in node.Children)
        {
            parent.Children.Add(ch);
            ch.Parent = parent;
        }

        this.dictionary.Remove(element);
    }

    public IEnumerable<T> GetChildren(T item)
    {
        if (!this.dictionary.ContainsKey(item))
        {
            throw new ArgumentException("Element does not exist!");
        }

        return this.dictionary[item].Children.Select(x => x.Value);
    }

    public T GetParent(T item)
    {
        if (!this.dictionary.ContainsKey(item))
        {
            throw new ArgumentException("Element does not exist!");
        }

        if (this.dictionary[item].Parent == null)
        {
            return default(T);
        }

        return this.dictionary[item].Parent.Value;
    }

    public bool Contains(T value)
    {
        return this.dictionary.ContainsKey(value);
    }

    public IEnumerable<T> GetCommonElements(Hierarchy<T> other)
    {
        return new HashSet<T>(this.dictionary.Keys).Intersect(other);
    }

    public IEnumerator<T> GetEnumerator()
    {
        var queue = new Queue<Node>();
        queue.Enqueue(this.root);

        while (queue.Count > 0)
        {
            var current = queue.Dequeue();

            foreach (var child in current.Children)
            {
                queue.Enqueue(child);
            }

            yield return current.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}