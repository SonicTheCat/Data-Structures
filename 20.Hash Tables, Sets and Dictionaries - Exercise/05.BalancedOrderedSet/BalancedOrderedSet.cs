using System;
using System.Collections;
using System.Collections.Generic;

public class BalancedOrderedSet<T> : IEnumerable<T>
     where T : IComparable<T>
{
    private AVLTree<T> tree;

    public BalancedOrderedSet()
    {
        this.tree = new AVLTree<T>(); 
    }

    public int Count => this.tree.Count;

    public void Add(T element)
    {
        this.tree.Add(element);
    }

    public bool Contains(T element)
    {
        return this.tree.Contains(element);
    }

    public void Remove(T element)
    {
        this.tree.Remove(element);
    }

    public IEnumerator<T> GetEnumerator()
    {
        var elements = new List<T>();
        tree.EachInOrder(elements.Add);

        foreach (var element in elements)
        {
            yield return element;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}