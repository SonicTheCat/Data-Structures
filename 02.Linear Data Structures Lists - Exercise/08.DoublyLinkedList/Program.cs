using System;

public class Program
{
    public static void Main()
    {
        var list = new DoublyLinkedList<int>();

        list.AddFirst(10);
        list.AddFirst(7);
        list.AddFirst(33);

        Console.WriteLine(list.RemoveFirst());

        list.ForEach(Console.WriteLine);
    }
}