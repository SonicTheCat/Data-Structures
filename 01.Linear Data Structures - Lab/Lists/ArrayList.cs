using System;

public class ArrayList<T>
{
    private const int CapacityIncreaserValue = 2;

    private T[] collection;
    private int capacity;
    private int count;

    public ArrayList(int collectionSize = CapacityIncreaserValue)
    {
        this.collection = new T[collectionSize];
        this.capacity = collectionSize;
    }

    public int Count
    {
        get
        {
            return this.count;
        }
        set
        {
            this.count = value;
        }
    }

    public T this[int index]
    {
        get
        {
            ValidateIndexIsInsideCollection(index);
            return this.collection[index];
        }

        set
        {
            ValidateIndexIsInsideCollection(index);
            this.collection[index] = value;
        }
    }

    public void Add(T item)
    {
        if (this.Count == this.capacity)
        {
            var cloningArray = new T[IncreaseCapacity()];
            this.collection.CopyTo(cloningArray, 0);
            this.collection = cloningArray;
        }
        this.collection[Count++] = item;
    }

    public T RemoveAt(int index)
    {
        ValidateIndexIsInsideCollection(index);

        if (this.Count - 1 <= this.capacity / 3)
        {
            Shrink();
        }

        var cloningArr = new T[this.capacity];
        T removedElement = default(T);
        var cloningIndexer = 0;

        for (int i = 0; i < this.Count; i++)
        {
            if (i == index)
            {
                removedElement = this.collection[i];
                continue;

            }
            cloningArr[cloningIndexer++] = this.collection[i];
        }

        this.Count--;
        this.collection = cloningArr;
        return removedElement;
    }

    private void ValidateIndexIsInsideCollection(int index)
    {
        if (index < 0 || index >= this.Count)
        {
            throw new ArgumentOutOfRangeException();
        }
    }

    private int IncreaseCapacity()
    {
        return this.capacity *= CapacityIncreaserValue;
    }

    private void Shrink()
    {
        this.capacity = this.Count;
    }
}