using System;

public class AVL<T> where T : IComparable<T>
{
    private Node<T> root;

    public Node<T> Root
    {
        get
        {
            return this.root;
        }
    }

    public void Insert(T item)
    {
        this.root = this.Insert(this.root, item);
    }

    private Node<T> Insert(Node<T> node, T item)
    {
        if (node == null)
        {
            return new Node<T>(item);
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            node.Left = this.Insert(node.Left, item);
        }
        else if (cmp > 0)
        {
            node.Right = this.Insert(node.Right, item);
        }

        node = CheckCurrentBalance(node);

        RecalculateHeight(node);

        return node;
    }

    private Node<T> CheckCurrentBalance(Node<T> node)
    {
        var balance = this.GetHeight(node.Left) - this.GetHeight(node.Right);

        if (balance > 1)
        {
            var childBalance = this.GetHeight(node.Left.Left) - this.GetHeight(node.Left.Right);

            //check if double rotation is needed first
            if (childBalance < 0)
            {
                node.Left = this.LeftRotation(node.Left);
            }

            
            node = this.RightRotation(node);
        }
        else if (balance < -1)
        {
            var childBalance = this.GetHeight(node.Right.Left) - this.GetHeight(node.Right.Right);

            //check if double rotation is needed first
            if (childBalance > 0)
            {
                node.Right = this.RightRotation(node.Right);
            }

            node = this.LeftRotation(node);
        }

        return node;
    }

    private Node<T> RightRotation(Node<T> node)
    {
        var temp = node.Left;
        node.Left = temp.Right;
        temp.Right = node;

        RecalculateHeight(node); 

        return temp; 
    }

    private Node<T> LeftRotation(Node<T> node)
    {
        var temp = node.Right;
        node.Right = temp.Left;
        temp.Left = node;

        RecalculateHeight(node);

        return temp;
    }
 
    private void RecalculateHeight(Node<T> node)
    {
        node.Height = 1 + Math.Max(this.GetHeight(node.Left), this.GetHeight(node.Right)); 
    }

    private int GetHeight(Node<T> node)
    {
        if (node == null)
        {
            return 0; 
        }

        return node.Height; 
    }

    public bool Contains(T item)
    {
        var node = this.Search(this.root, item);
        return node != null;
    }

    private Node<T> Search(Node<T> node, T item)
    {
        if (node == null)
        {
            return null;
        }

        int cmp = item.CompareTo(node.Value);
        if (cmp < 0)
        {
            return Search(node.Left, item);
        }
        else if (cmp > 0)
        {
            return Search(node.Right, item);
        }

        return node;
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Value);
        this.EachInOrder(node.Right, action);
    }
}