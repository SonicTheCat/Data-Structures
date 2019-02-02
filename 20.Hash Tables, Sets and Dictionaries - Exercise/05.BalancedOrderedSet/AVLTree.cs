using System;
using System.Collections;
using System.Collections.Generic;

public class AVLTree<T>
    where T : IComparable<T>
{
    private Node root;

    public AVLTree()
    {
        this.root = null;
    }

    public int Count { get; private set; }

    public void Add(T element)
    {
        this.root = this.Add(this.root, element);
    }

    private Node Add(Node node, T element)
    {
        if (node == null)
        {
            this.Count++;
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

        node = this.CheckCurrentBalance(node);
        this.RecalculateHeight(node);

        return node;
    }

    private Node CheckCurrentBalance(Node node)
    {
        var balance = this.GetHeight(node.Left) - this.GetHeight(node.Right);
        if (balance < -1)
        {
            var childBalance = this.GetHeight(node.Right.Left) - this.GetHeight(node.Right.Right);
            if (childBalance > 0)
            {
                node.Right = this.RotateRight(node.Right);
            }
            node = this.RotateLeft(node);
        }
        else if (balance > 1)
        {
            var childBalance = this.GetHeight(node.Left.Left) - this.GetHeight(node.Left.Right);
            if (childBalance < 0)
            {
                node.Left = this.RotateLeft(node.Left);
            }
            node = this.RotateRight(node);
        }

        return node;
    }

    private Node RotateLeft(Node node)
    {
        var temp = node.Right;
        node.Right = temp.Left;
        temp.Left = node;
        this.RecalculateHeight(node);

        return temp;
    }

    private Node RotateRight(Node node)
    {
        var temp = node.Left;
        node.Left = temp.Right;
        temp.Right = node;
        this.RecalculateHeight(node);

        return temp;
    }

    private void RecalculateHeight(Node node)
    {
        node.Height = 1 + Math.Max(this.GetHeight(node.Left), this.GetHeight(node.Right));
    }

    private int GetHeight(Node node)
    {
        if (node == null)
        {
            return 0;
        }

        return node.Height;
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
        var isDeleted = false;
        this.root = this.Remove(this.root, element, ref isDeleted);

        if (isDeleted)
        {
            this.Count--;
        }
    }

    private Node Remove(Node node, T element, ref bool isDeleted)
    {
        if (node == null)
        {
            return null;
        }

        var cmp = node.Value.CompareTo(element);
        if (cmp > 0)
        {
            node.Left = this.Remove(node.Left, element, ref isDeleted);
        }
        else if (cmp < 0)
        {
            node.Right = this.Remove(node.Right, element,ref isDeleted);
        }
        else
        {
            isDeleted = true; 

            if (node.Right == null)
            {
                return node.Left;
            }
            else if (node.Left == null)
            {
                return node.Right;
            }
            else
            {
                var temp = FindLeftMostChild(node.Right);
                this.Remove(node, temp.Value, ref isDeleted);

                temp.Left = node.Left;
                temp.Right = node.Right;
                node = temp;
            }
        }

        this.RecalculateHeight(node);
        node = this.CheckCurrentBalance(node);
        this.RecalculateHeight(node);

        return node;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(action, this.root);
    }

    private void EachInOrder(Action<T> action, Node node)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(action, node.Left);
        action(node.Value);
        this.EachInOrder(action, node.Right);
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

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
            this.Height = 1;
        }

        public T Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public int Height { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }
}