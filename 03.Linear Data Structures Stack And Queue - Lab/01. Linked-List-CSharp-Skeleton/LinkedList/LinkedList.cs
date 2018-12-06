using System;
using System.Collections;
using System.Collections.Generic;

public class LinkedList<T> : IEnumerable<T>
{
    public Node Head { get; set; }

    public Node Tail { get; set; }

    public int Count { get; private set; }

    public void AddFirst(T item)
    {
        if (this.Count == 0)
        {
            this.Head = this.Tail = new Node(item);
        }
        else
        {
            var newItem = new Node(item);
            newItem.Next = this.Head;
            this.Head = newItem;
        }
        this.Count++;
    }

    public void AddLast(T item)
    {
        if (this.Count == 0)
        {
            this.Head = this.Tail = new Node(item);
        }
        else
        {
            var currentTail = this.Tail;
            this.Tail = new Node(item);
            currentTail.Next = this.Tail;
        }
        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }
        else if (this.Count == 1)
        {
            this.Tail = null;
        }

        var item = this.Head.Value;
        this.Head = this.Head.Next;
        this.Count--;
        return item;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException();
        }

        var item = this.Tail.Value;
        if (this.Count == 1)
        {
            this.Head = this.Tail = null;
        }
        else
        {
            var currentNode = GetElementBeforeLast();
            currentNode.Next = null; 
            this.Tail = currentNode;
        }
        this.Count--;
        return item;
    }

    public IEnumerator<T> GetEnumerator()
    {
        var currentNode = this.Head;
        while (currentNode != null)
        {
            yield return currentNode.Value;
            currentNode = currentNode.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public class Node
    {
        public Node(T element)
        {
            this.Value = element;
        }

        public T Value { get; set; }

        public Node Next { get; set; }
    }

    private Node GetElementBeforeLast()
    {
        var currentNode = this.Head;
        while (currentNode.Next != this.Tail)
        {
            currentNode = currentNode.Next;
        }

        return currentNode;
    }
}