using System;

class Program
{
    static void Main(string[] args)
    {
        AVL<int> tree = new AVL<int>();
        tree.Insert(18);
        tree.Insert(33);
        tree.Insert(29);
        tree.Insert(9);
        tree.Insert(2);
        tree.Insert(5);
        tree.Insert(54);
        tree.Insert(14);
        tree.Insert(99);

        tree.DeleteMin();
        tree.Delete(29);

        Console.WriteLine();
    }
}
