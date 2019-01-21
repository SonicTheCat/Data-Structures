using System;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class FirstLastList<T> : IFirstLastList<T>
    where T : IComparable<T>
{
    private readonly OrderedBag<LinkedListNode<T>> orderedAsc;
    private readonly OrderedBag<LinkedListNode<T>> orderedDesc;
    private readonly LinkedList<T> byInsertion;

    public FirstLastList()
    {
        this.byInsertion = new LinkedList<T>();
        this.orderedAsc = new OrderedBag<LinkedListNode<T>>((x, y) => x.Value.CompareTo(y.Value));
        this.orderedDesc = new OrderedBag<LinkedListNode<T>>((x, y) => y.Value.CompareTo(x.Value));     
    }

    public int Count => this.byInsertion.Count;

    private bool IsCountValid(int count) => count <= this.Count && count >= 0;

    public void Add(T element)
    {
        LinkedListNode<T> node = new LinkedListNode<T>(element); 
        this.orderedAsc.Add(node);
        this.orderedDesc.Add(node); 
        this.byInsertion.AddLast(node);
    }

    public void Clear()
    {
        this.orderedAsc.Clear();
        this.orderedDesc.Clear(); 
        this.byInsertion.Clear();
    }

    public IEnumerable<T> First(int count)
    {
        if (!IsCountValid(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        var current = this.byInsertion.First;

        while (count-- > 0)
        {
            yield return current.Value;

            current = current.Next;
        }
    }

    public IEnumerable<T> Last(int count)
    {
        if (!IsCountValid(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        var current = this.byInsertion.Last;

        while (count-- > 0)
        {
            yield return current.Value;

            current = current.Previous;
        }
    }

    public IEnumerable<T> Max(int count)
    {
        if (!IsCountValid(count))
        {
            throw new ArgumentOutOfRangeException();
        }

       return this.orderedDesc.Take(count).Select(x => x.Value).ToArray(); 
    }

    public IEnumerable<T> Min(int count)
    {
        if (!IsCountValid(count))
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.orderedAsc.Take(count).Select(x => x.Value).ToArray();
    }

    public int RemoveAll(T element)
    {
        var node = new LinkedListNode<T>(element);

        var elementsToRemove = this.orderedAsc.Range(node, true, node, true);

        foreach (var item in elementsToRemove)
        {
            this.byInsertion.Remove(item); 
        }

        this.orderedAsc.RemoveAllCopies(node);

        return this.orderedDesc.RemoveAllCopies(node); 
    }
}