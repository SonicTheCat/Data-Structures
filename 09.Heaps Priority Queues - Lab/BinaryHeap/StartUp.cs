using System;

public class StartUp
{
    static void Main()
    {
        //Console.WriteLine("Created an empty heap.");
        var heap = new BinaryHeap<int>();
        //heap.Insert(5);
        //heap.Insert(8);
        //heap.Insert(1);
        //heap.Insert(3);
        //heap.Insert(12);
        //heap.Insert(-4);

        //Console.WriteLine("Heap elements (max to min):");
        //while (heap.Count > 0)
        //{
        //    var max = heap.Pull();
        //    Console.WriteLine(max);
        //}

        //heap.Insert(30);
        //heap.Insert(20);
        //heap.Insert(100);

        //heap.Insert(15);
        //heap.Insert(14);
        //heap.Insert(8);
        //heap.Insert(12);
        //heap.Insert(10);

        //heap.Insert(5);
        //heap.Insert(3);
        //heap.Insert(1);

        //var deletedElement = heap.Pull();
        //deletedElement = heap.Pull();
        //deletedElement = heap.Pull();
        //Console.WriteLine(deletedElement);

        var arr = new[] { 100, -11, 12, 300, 1 };
        Heap<int>.Sort(arr); 
    }
}
