using System;
using System.Collections.Generic;

public class BinarySearchTree<T> where T : IComparable<T>
{
    private Node root;

    public BinarySearchTree()
    {

    }

    private BinarySearchTree(Node subTreeRoot)
    {
        this.Copy(subTreeRoot);
    }

    private void Copy(Node subTreeRoot)
    {
        if (subTreeRoot == null)
        {
            return;
        }

        this.Insert(subTreeRoot.Value);
        this.Copy(subTreeRoot.Left);
        this.Copy(subTreeRoot.Right);
    }

    private class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }

        public override string ToString()
        {
            return this.Value.ToString();
        }
    }

    public void Insert(T value)
    {
        if (this.root == null)
        {
            this.root = new Node(value);
            return;
        }

        Node current = this.root;
        Node parent = null;

        while (current != null)
        {
            parent = current;
            if (current.Value.CompareTo(value) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(value) < 0)
            {
                current = current.Right;
            }
            else
            {
                return;
            }
        }

        var newNode = new Node(value);
        if (parent.Value.CompareTo(value) > 0)
        {
            parent.Left = newNode;
        }
        else
        {
            parent.Right = newNode;
        }
    }

    public bool Contains(T value)
    {
        Node current = this.root;

        while (current != null)
        {
            if (current.Value.CompareTo(value) > 0)
            {
                current = current.Left;
            }
            else if (current.Value.CompareTo(value) < 0)
            {
                current = current.Right;
            }
            else
            {
                return true;
            }
        }

        return false;
    }

    public void DeleteMin()
    {
        if (this.root == null)
        {
            return;
        }

        Node parent = null;
        Node current = this.root;

        while (current.Left != null)
        {
            parent = current;
            current = current.Left;
        }

        if (parent == null)
        {
            this.root = this.root.Right;
        }
        else
        {
            parent.Left = current.Right;
        }
    }

    public BinarySearchTree<T> Search(T item)
    {
        Node subTreeRoot = this.root;

        while (subTreeRoot != null)
        {
            if (subTreeRoot.Value.CompareTo(item) > 0)
            {
                subTreeRoot = subTreeRoot.Left;
            }
            else if (subTreeRoot.Value.CompareTo(item) < 0)
            {
                subTreeRoot = subTreeRoot.Right;
            }
            else
            {
                break;
            }
        }

        if (subTreeRoot == null)
        {
            return new BinarySearchTree<T>();
        }

        return new BinarySearchTree<T>(subTreeRoot);
    }

    public IEnumerable<T> Range(T startRange, T endRange)
    {
        var list = new List<T>();

        Range(this.root, list.Add, startRange, endRange);

        return list; 
    }

    private void Range(Node current, Action<T> action, T start, T end)
    {
        if (current == null)
        {
            return;
        }

        Range(current.Left, action, start, end);

        if (current.Value.CompareTo(start) >= 0 && current.Value.CompareTo(end) <= 0)
        {
            action(current.Value);  
        }

        Range(current.Right, action, start, end);
    }

    public void EachInOrder(Action<T> action)
    {
        EachInOrder(this.root, action);
    }

    private void EachInOrder(Node current, Action<T> action)
    {
        if (current == null)
        {
            return;
        }

        EachInOrder(current.Left, action);
        action(current.Value);
        EachInOrder(current.Right, action);
    }
}

public class Launcher
{
    public static void Main(string[] args)
    {
        BinarySearchTree<int> binarySearchTree = new BinarySearchTree<int>();

        binarySearchTree.Insert(20);
        binarySearchTree.Insert(100);
        binarySearchTree.Insert(10);
        binarySearchTree.Insert(15);
        binarySearchTree.Insert(7);
        binarySearchTree.Insert(7);

        binarySearchTree.Insert(5);
        binarySearchTree.Insert(5);
        binarySearchTree.Insert(8);
        binarySearchTree.Insert(88);
        binarySearchTree.Insert(150);
        binarySearchTree.Insert(125);
        binarySearchTree.Insert(175);
        binarySearchTree.Insert(200);

        binarySearchTree.Contains(100);
        binarySearchTree.Contains(1000000);

        binarySearchTree.EachInOrder(Console.WriteLine);

        binarySearchTree.Search(7);
        binarySearchTree.Search(175);
        binarySearchTree.Search(30);

        //binarySearchTree.DeleteMin();
        //binarySearchTree.DeleteMin();
        //binarySearchTree.DeleteMin();
        //binarySearchTree.DeleteMin();
        //binarySearchTree.DeleteMin();
        //binarySearchTree.DeleteMin();
        //binarySearchTree.DeleteMin();

        binarySearchTree.Range(7, 150);
    }
}
