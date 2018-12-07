using System;

public class ArrayStack<T>
{
    private const int InitialCapacity = 16; 

    private T[] stack;

    public ArrayStack(int capacity = InitialCapacity)
    {
        this.stack = new T[capacity]; 
    }

    public int Count { get; private set; }

    public void Push(T element)
    {
        if (this.Count == this.stack.Length)
        {
            Grow();
        }

        this.stack[this.Count++] = element; 
    }

    public T Pop()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("Stack is empty!"); 
        }

        var element = this.stack[--this.Count];

        return element; 
    }

    public T[] ToArray()
    {
        var arr = new T[this.Count];
        var index = this.Count - 1; 

        for (int i = 0; i < this.Count; i++)
        {
            arr[i] = this.stack[index--]; 
        }

        return arr; 
    }

    private void Grow()
    {
        T[] cloningArr = new T[this.stack.Length * 2];
        this.stack.CopyTo(cloningArr, 0);
        this.stack = cloningArr;
    }
}