using System;
using System.Collections.Generic;

public class BinaryHeap<T>
    where T : IComparable<T>
{
    private readonly List<T> heap;

    public BinaryHeap()
    {
        this.heap = new List<T>();
    }

    public int Count => this.heap.Count;

    public void Insert(T item)
    {
        this.heap.Add(item);

        this.HeapifyUp(this.heap.Count - 1);
    }

    private void HeapifyUp(int lastIndex)
    {
        var parentIndex = (lastIndex - 1) / 2;

        while (lastIndex > 0 && IsGreater(this.heap[lastIndex], this.heap[parentIndex]))
        {
            SwapElements(lastIndex, parentIndex);

            lastIndex = parentIndex;
            parentIndex = (lastIndex - 1) / 2;
        }
    }

    private void SwapElements(int childIndex, int parentIndex)
    {
        var temp = this.heap[childIndex];
        this.heap[childIndex] = this.heap[parentIndex];
        this.heap[parentIndex] = temp;
    }

    private bool IsGreater(T a, T b)
    {
        return a.CompareTo(b) > 0;
    }

    public T Peek()
    {
        if (this.heap.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        return this.heap[0];
    }

    public T Pull()
    {
        if (this.heap.Count <= 0)
        {
            throw new InvalidOperationException();
        }

        var deletedElement = this.heap[0];

        SwapElements(this.heap.Count - 1, 0);
        this.heap.RemoveAt(this.heap.Count - 1);

        HeapifyDown(0);

        return deletedElement;
    }

    private void HeapifyDown(int parentIndex)
    {
        while (parentIndex < this.heap.Count / 2)
        {
            int child = 2 * parentIndex + 1;

            if (child + 1 < this.Count && IsGreater(this.heap[child + 1], this.heap[child]))
            {
                child++;
            }

            if (!this.IsGreater(this.heap[child], this.heap[parentIndex]))
            {
                break; 
            }

            SwapElements(child, parentIndex);

            parentIndex = child;
        }
    }
}