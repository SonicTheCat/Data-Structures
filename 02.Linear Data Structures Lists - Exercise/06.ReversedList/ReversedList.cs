﻿namespace ReversedList
{

    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ReversedList<T> : IEnumerable<T>
    {
        private const int CapacityIncreaserValue = 2;

        private T[] collection;
        private int capacity;
        private int count;

        public ReversedList(int collectionSize = CapacityIncreaserValue)
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

        public int Capacity
        {
            get
            {
                return this.capacity;
            }
            set
            {
                this.capacity = value;
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
            if (this.Count == this.Capacity)
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

            if (this.Count - 1 <= this.Capacity / 3)
            {
                Shrink();
            }

            var cloningArr = new T[this.Capacity];
            T removedElement = default(T);
            var cloningIndexer = 0;

            for (int i = 0; i < this.Count; i++)
            {
                if (i == this.Count - 1 - index)
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
            return this.Capacity *= CapacityIncreaserValue;
        }

        private void Shrink()
        {
            this.Capacity /= 2;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                yield return this.collection[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
    }
}