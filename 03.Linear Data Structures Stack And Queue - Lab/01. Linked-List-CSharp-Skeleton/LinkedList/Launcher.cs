class Launcher
{
    public static void Main()
    {
        LinkedList<int> list = new LinkedList<int>();

        list.AddFirst(10);
        list.AddFirst(1);
        list.AddLast(500);
        list.AddLast(600);

        foreach (var item in list)
        {
            System.Console.WriteLine(item);
        }

        System.Console.WriteLine("----------------------------");

        list.RemoveFirst();

        foreach (var item in list)
        {
            System.Console.WriteLine(item);
        }

        list.RemoveLast();
        System.Console.WriteLine("----------------------------");
        foreach (var item in list)
        {
            System.Console.WriteLine(item);
        }
    }
}
