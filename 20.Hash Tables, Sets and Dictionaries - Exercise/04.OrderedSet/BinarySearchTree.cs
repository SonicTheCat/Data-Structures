using System;
using System.Collections;
using System.Collections.Generic;

public class BinarySearchTree<T> : IEnumerable<T>
    where T : IComparable<T>
{
    private Node root;

    public BinarySearchTree()
    {
        this.root = null;
    }

    public int Count
    {
        get
        {
            if (this.root == null)
            {
                return 0;
            }
            return this.root.Count;
        }
    }

    public void Add(T element)
    {
        this.root = this.Add(this.root, element);
    }

    private Node Add(Node node, T element)
    {
        if (node == null)
        {
            return new Node(element);
        }

        var cmp = node.Value.CompareTo(element);
        if (cmp > 0)
        {
            node.Left = this.Add(node.Left, element);
        }
        else if (cmp < 0)
        {
            node.Right = this.Add(node.Right, element);
        }

        node.Count = 1 + this.GetNodeCount(node.Left) + this.GetNodeCount(node.Right);
        return node;
    }

    public bool Contains(T element)
    {
        return this.Contains(this.root, element);
    }

    private bool Contains(Node node, T element)
    {
        if (node == null)
        {
            return false;
        }

        var cmp = node.Value.CompareTo(element);

        if (cmp > 0)
        {
            return this.Contains(node.Left, element);
        }
        else if (cmp < 0)
        {
            return this.Contains(node.Right, element);
        }

        return true;
    }

    public void Remove(T element)
    {
        this.root = this.Remove(this.root, element);
    }

    private Node Remove(Node node, T element)
    {
        if (node == null)
        {
            return null;
        }

        var cmp = node.Value.CompareTo(element);
        if (cmp > 0)
        {
            node.Left = this.Remove(node.Left, element);
        }
        else if (cmp < 0)
        {
            node.Right = this.Remove(node.Right, element);
        }
        else
        {
            if (node.Right == null)
            {
                return node.Left;
            }
            else if (node.Left == null)
            {
                return node.Right;
            }

            var temp = FindLeftMostChild(node.Right);
            this.Remove(node, temp.Value);

            temp.Left = node.Left;
            temp.Right = node.Right;
            node = temp;
        }

        node.Count = 1 + this.GetNodeCount(node.Left) + this.GetNodeCount(node.Right);

        return node;
    }

    private Node FindLeftMostChild(Node node)
    {
        if (node == null)
        {
            return null;
        }

        while (node.Left != null)
        {
            node = node.Left;
        }

        return node;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var elements = new List<T>();
        this.SortElements(elements, this.root);

        foreach (var element in elements)
        {
            yield return element;
        }
    }

    private void SortElements(List<T> elements, Node node)
    {
        if (node == null)
        {
            return;
        }

        this.SortElements(elements, node.Left);
        elements.Add(node.Value);
        this.SortElements(elements, node.Right); 
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    private int GetNodeCount(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Count;
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
            this.Count = 1;
        }

        public T Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public int Count { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}