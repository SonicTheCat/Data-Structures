using System;
using System.Collections;
using System.Collections.Generic;

public class DoublyLinkedList<T> : IEnumerable<T>
{
    private class ListNode<T>
    {
        public T Value { get; private set; }

        public ListNode<T> NextNode { get; set; }

        public ListNode<T> PrevNode { get; set; }

        public ListNode(T value)
        {
            this.Value = value;
        }
    }

    private ListNode<T> head;
    private ListNode<T> tail;

    public int Count { get; private set; }

    public void AddFirst(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var newHead = new ListNode<T>(element);
            newHead.NextNode = this.head;
            this.head.PrevNode = newHead;
            this.head = newHead;
        }
        this.Count++;
    }

    public void AddLast(T element)
    {
        if (this.Count == 0)
        {
            this.head = this.tail = new ListNode<T>(element);
        }
        else
        {
            var newTail = new ListNode<T>(element);
            newTail.PrevNode = this.tail;
            this.tail.NextNode = newTail;
            this.tail = newTail;
        }
        this.Count++;
    }

    public T RemoveFirst()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List empty");
        }

        var nodeToRemove = this.head;
        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.head = nodeToRemove.NextNode;
            this.head.PrevNode = null;
        }
        this.Count--;
        return nodeToRemove.Value;
    }

    public T RemoveLast()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("List empty");
        }

        var nodeToRemove = this.tail;

        if (this.Count == 1)
        {
            this.head = this.tail = null;
        }
        else
        {
            this.tail = nodeToRemove.PrevNode;
            this.tail.NextNode = null;
        }

        this.Count--;
        return nodeToRemove.Value;
    }

    public void ForEach(Action<T> action)
    {
        var currentNode = this.head;
        while (currentNode != null)
        {
            action(currentNode.Value);
            currentNode = currentNode.NextNode;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        T[] arr = this.ToArray();

        for (int i = 0; i < arr.Length; i++)
        {
            yield return arr[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }

    public T[] ToArray()
    {
        T[] arr = new T[this.Count];

        var currentNode = this.head;
        var counter = 0;
        while (currentNode != null)
        {
            arr[counter++] = currentNode.Value;
            currentNode = currentNode.NextNode;
        }
        return arr;
    }
}