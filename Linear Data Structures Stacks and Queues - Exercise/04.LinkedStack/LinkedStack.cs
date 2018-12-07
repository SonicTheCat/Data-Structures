using System;

public class LinkedStack<T>
{
    private Node top;

    public int Count { get; private set; }

    public void Push(T element)
    {
        this.top = new Node(element, this.top);
        this.Count++;
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty!");
        }

        var value = this.top.Value;
        this.top = this.top.Next;
        this.Count--;
        return value;
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];
        var index = 0;
        var current = this.top;

        while (current != null)
        {
            arr[index++] = current.Value;
            current = current.Next;
        }

        return arr;
    }

    public class Node
    {
        public Node(T value, Node nextNode)
        {
            this.Value = value;
            this.Next = nextNode;
        }

        public T Value { get; }

        public Node Next { get; set; }
    }
}