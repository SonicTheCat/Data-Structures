using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;

public class HashTable<TKey, TValue> : IEnumerable<KeyValue<TKey, TValue>>
{
    private const int defaultCapacity = 16;
    private const float LoadFactorMax = 0.75f;

    private LinkedList<KeyValue<TKey, TValue>>[] slots;

    public HashTable()
    : this(defaultCapacity)
    {
    }

    public HashTable(int capacity)
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[capacity];
        this.Count = 0;
    }

    public int Count { get; private set; }

    public int Capacity => this.slots.Length;

    public double CurrentLoadFactor => (double)(this.Count + 1) / this.Capacity;

    public void Add(TKey key, TValue value)
    {
        ResizeIfNeeded();
        int slotNumber = GetSlotNumber(key);

        if (this.slots[slotNumber] == null)
        {
            this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var item in this.slots[slotNumber])
        {
            if (item.Key.Equals(key))
            {
                throw new ArgumentException("Given key already exists");
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        this.slots[slotNumber].AddLast(newElement);
        this.Count++;
    }

    public bool AddOrReplace(TKey key, TValue value)
    {
        ResizeIfNeeded();
        int slotNumber = GetSlotNumber(key);

        if (this.slots[slotNumber] == null)
        {
            this.slots[slotNumber] = new LinkedList<KeyValue<TKey, TValue>>();
        }

        foreach (var item in this.slots[slotNumber])
        {
            if (item.Key.Equals(key))
            {
                item.Value = value;
                return false;
            }
        }

        var newElement = new KeyValue<TKey, TValue>(key, value);
        this.slots[slotNumber].AddLast(newElement);
        this.Count++;
        return true;
    }

    public TValue Get(TKey key)
    {
        var kvp = this.Find(key);
        if (kvp == null)
        {
            throw new KeyNotFoundException($"Key + \"{key}\" does not exist");
        }
        return kvp.Value;
    }

    public TValue this[TKey key]
    {
        get
        {
            return this.Get(key); 
        }
        set
        {
            this.AddOrReplace(key, value);
        }
    }

    public bool TryGetValue(TKey key, out TValue value)
    {
        var kvp = this.Find(key);
        if (kvp != null)
        {
            value = kvp.Value;
            return true;
        }

        value = default(TValue);
        return false;
    }

    public KeyValue<TKey, TValue> Find(TKey key)
    {
        var slotNumber = this.GetSlotNumber(key);
        var elements = this.slots[slotNumber];

        if (elements != null)
        {
            foreach (var element in elements)
            {
                if (element.Key.Equals(key))
                {
                    return element;
                }
            }
        }
        return null;
    }

    public bool ContainsKey(TKey key)
    {
        var kvp = this.Find(key);
        return kvp != null;
    }

    public bool Remove(TKey key)
    {
        var slotNumber = this.GetSlotNumber(key);
        var kvp = this.Find(key);
        if (this.slots[slotNumber] == null || kvp == null)
        {
            return false; 
        }

        this.slots[slotNumber].Remove(kvp);
        this.Count--;
        return true;
    }

    public void Clear()
    {
        this.slots = new LinkedList<KeyValue<TKey, TValue>>[defaultCapacity];
        this.Count = 0;
    }

    public IEnumerable<TKey> Keys
    {
        get
        {
            //foreach (var kvp in this)
            //{
            //    yield return kvp.Key;
            //}
            return this.Select(x => x.Key); 
        }
    }

    public IEnumerable<TValue> Values
    {
        get
        {
            return this.Select(x => x.Value);
        }
    }

    public IEnumerator<KeyValue<TKey, TValue>> GetEnumerator()
    {
        foreach (var list in this.slots)
        {
            if (list != null)
            {
                foreach (var kvp in list)
                {
                    yield return kvp;
                }
            }
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

    private int GetSlotNumber(TKey key) => Math.Abs(key.GetHashCode()) % this.slots.Length;

    private void ResizeIfNeeded()
    {
        if (this.CurrentLoadFactor > LoadFactorMax)
        {
            this.Grow();
        }
    }

    private void Grow()
    {
        var newHashtable = new HashTable<TKey, TValue>(this.Capacity * 2);

        foreach (var list in this.slots)
        {
            if (list != null)
            {
                foreach (var kvp in list)
                {
                    newHashtable.Add(kvp.Key, kvp.Value);
                }
            }
        }
        this.slots = newHashtable.slots;
        this.Count = newHashtable.Count;
    }
}
