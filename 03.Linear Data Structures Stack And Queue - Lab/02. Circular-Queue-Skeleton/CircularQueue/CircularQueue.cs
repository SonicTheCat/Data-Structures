using System;

public class CircularQueue<T>
{
    private const int InitialCapacity = 16;

    private T[] elements;
    private int startIndex;
    private int endIndex;

    public int Count { get; private set; }

    public CircularQueue(int capacity = InitialCapacity)
    {
        this.elements = new T[capacity];
    }

    public void Enqueue(T element)
    {
        if (this.Count >= this.elements.Length)
        {
            Grow();
        }

        this.elements[this.endIndex] = element;
        this.endIndex = (this.endIndex + 1) % this.elements.Length;
        this.Count++; 
    }

    private void Grow()
    {
        var cloningArray = new T[this.elements.Length * 2];
        this.CopyAllElements(cloningArray);
        this.elements = cloningArray;
        this.startIndex = 0;
        this.endIndex = this.Count; 
    }

    private void CopyAllElements(T[] newArray)
    {
        var sourceIndex = this.startIndex;
        var destinationIndex = 0;

        for (int i = 0; i < this.Count; i++)
        {
            newArray[destinationIndex++] = this.elements[sourceIndex];
            sourceIndex = (sourceIndex + 1) % this.elements.Length; 
        }
    }

    // Should throw InvalidOperationException if the queue is empty
    public T Dequeue()
    {
        if (this.Count == 0)
        {
            throw new InvalidOperationException("The queue is empty!"); 
        }

        var result = this.elements[startIndex];
        this.startIndex = (this.startIndex + 1) % this.elements.Length;
        this.Count--;
        return result; 
    }

    public T[] ToArray()
    {
        var queToArr = new T[this.Count];
        this.CopyAllElements(queToArr);
        return queToArr; 
    }
}

public class Example
{
    public static void Main()
    {
        var que = new CircularQueue<int>();

        que.Enqueue(20);
        que.Enqueue(1);
        que.Enqueue(-123);
        que.Enqueue(0);

        que.Dequeue();
        que.Dequeue();

        que.Enqueue(18);
        que.Enqueue(4);
        que.Enqueue(3);

        //CircularQueue<int> queue = new CircularQueue<int>();

        //queue.Enqueue(1);
        //queue.Enqueue(2);
        //queue.Enqueue(3);
        //queue.Enqueue(4);
        //queue.Enqueue(5);
        //queue.Enqueue(6);

        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //int first = queue.Dequeue();
        //Console.WriteLine("First = {0}", first);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //queue.Enqueue(-7);
        //queue.Enqueue(-8);
        //queue.Enqueue(-9);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //first = queue.Dequeue();
        //Console.WriteLine("First = {0}", first);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //queue.Enqueue(-10);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");

        //first = queue.Dequeue();
        //Console.WriteLine("First = {0}", first);
        //Console.WriteLine("Count = {0}", queue.Count);
        //Console.WriteLine(string.Join(", ", queue.ToArray()));
        //Console.WriteLine("---------------------------");
    }
}