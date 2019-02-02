using System;

public class StartUp
{
    public static void Main()
    {
        var set = new OrderedSet<int>();

        set.Add(190);
        set.Add(90);
        set.Add(10);
        set.Add(1);
        set.Add(-6);
        set.Add(-20);
        set.Add(-219);
        set.Add(-300);

        foreach (var item in set)
        {
            Console.WriteLine(item);
        }
    }
}