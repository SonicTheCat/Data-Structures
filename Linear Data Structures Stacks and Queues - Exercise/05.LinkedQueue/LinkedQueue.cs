using System;

public class LinkedQueue<T>
{
    private Node head;
    private Node tail;

    public int Count { get; private set; }

    public void Enqueue(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new Node(element);
        }
        else
        {
            var newTail = new Node(element);
            newTail.Prev = this.tail; 
            this.tail.Next = newTail;
            this.tail = newTail; 

        }
        this.Count++;
    }

    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var value = this.head.Value;
        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.head = this.head.Next;
            this.head.Prev = null;
        }
        this.Count--;
        return value;
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];
        var index = 0;
        var current = this.head;

        while (current != null)
        {
            arr[index++] = current.Value;
            current = current.Next;
        }

        return arr;
    }

    public class Node
    {
        public Node(T value)
        {
            this.Value = value;
        }

        public T Value { get; }

        public Node Next { get; set; }

        public Node Prev { get; set; }
    }
}