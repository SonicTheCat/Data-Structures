using System;
using System.Collections.Generic;

public class OrderedSet<T> 
    where T : IComparable<T>
{
    private BinarySearchTree<T> bst;

    public OrderedSet()
    {
        this.bst = new BinarySearchTree<T>(); 
    }

    public int Count => this.bst.Count;

    public void Add(T element)
    {
        this.bst.Add(element);
    }

    public bool Contains(T element)
    {
        return this.bst.Contains(element);
    }

    public void Remove(T element)
    {
        this.bst.Remove(element);
    }

    public IEnumerator<T> GetEnumerator() => bst.GetEnumerator();
}