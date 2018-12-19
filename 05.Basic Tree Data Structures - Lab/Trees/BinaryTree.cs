using System;

public class BinaryTree<T>
{
    public BinaryTree(T value, BinaryTree<T> leftChild = null, BinaryTree<T> rightChild = null)
    {
        this.Value = value;
        this.Left = leftChild;
        this.Right = rightChild;
    }

    public T Value { get; }

    public BinaryTree<T> Left { get; }

    public BinaryTree<T> Right { get; }

    public void PrintIndentedPreOrder(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + this.Value);

        if (this.Left != null)
        {
            this.Left.PrintIndentedPreOrder(indent + 2);
        }

        if (this.Right != null)
        {
            this.Right.PrintIndentedPreOrder(indent + 2);
        }
    }

    public void EachInOrder(Action<T> action)
    {
        this.EachInOrder(this, action);
    }

    private void EachInOrder(BinaryTree<T> node, Action<T> action)
    {
        if (node == null)
        {
            return; 
        }

        EachInOrder(node.Left, action);
        action(node.Value);
        EachInOrder(node.Right, action); 
    }

    public void EachPostOrder(Action<T> action)
    {
        this.EachPostOrder(this, action); 
    }

    private void EachPostOrder(BinaryTree<T> node, Action<T> action)
    {
        if (node == null)
        {
            return;
        }

        EachPostOrder(node.Left, action);
        EachPostOrder(node.Right, action);
        action(node.Value);
    }
}
