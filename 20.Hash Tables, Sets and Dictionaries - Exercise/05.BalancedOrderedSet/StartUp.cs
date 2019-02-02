using System;

public class StartUp
{
    public static void Main()
    {
        var set = new BalancedOrderedSet<int>();

        set.Add(190);
        set.Add(90);
        set.Add(80);
        set.Add(110);
        set.Add(500);
        set.Add(36);
        set.Add(20);
        set.Add(-10);
        set.Add(3);

        foreach (var item in set)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("Set Count => " + set.Count);

        set.Remove(90);
        set.Remove(90); //No

        set.Remove(80);
        set.Remove(120); //No
        set.Remove(500);
        set.Remove(36);

        Console.WriteLine(new string('-', 20));

        foreach (var item in set)
        {
            Console.WriteLine(item);
        }

        Console.WriteLine("Set Count => " + set.Count);
    }
}