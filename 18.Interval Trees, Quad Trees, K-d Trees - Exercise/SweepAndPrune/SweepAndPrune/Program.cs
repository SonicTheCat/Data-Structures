using System;
using System.Collections.Generic;

public class Program
{
    public static void Main()
    {
        var items = new List<Item>();
        var itemsById = new Dictionary<string, Item>();

        while (true)
        {
            var tokens = Console.ReadLine().Split();
            var command = tokens[0];

            switch (command)
            {
                case "add":
                    AddItem(items, itemsById, tokens);
                    break;
                case "start":
                    StartGame(items, itemsById);
                    return; 
            }
        }
    }

    private static void StartGame(List<Item> items, Dictionary<string, Item> itemsById)
    {
        var ticks = 1; 

        while (true)
        {
            var tokens = Console.ReadLine().Split();
            var command = tokens[0];

            if (command == "end")
            {
                return;
            }

            if (command == "move")
            {
                UpdateValues(itemsById, tokens);
            }

            InsertionSort(items);
            CheckIfIntersects(items, ticks++);
        }
    }

    private static void CheckIfIntersects(List<Item> items, int ticks)
    {
        for (int i = 0; i < items.Count; i++)
        {
            var currentItem = items[i];
            for (int j = i + 1; j < items.Count; j++)
            {
                var candidate = items[j];
                if (currentItem.X2 < candidate.X1)
                {
                    break;
                }

                if (currentItem.Intersect(candidate))
                {
                    Console.WriteLine($"({ticks}) {currentItem.Name} collides with {candidate.Name}");
                }
            }
        }
    }

    private static void InsertionSort(List<Item> items)
    {
        for (int i = 1; i < items.Count; i++)
        {
            var j = i - 1;
            while (j >= 0 && items[j].X1 > items[j + 1].X1)
            {
                var temp = items[j];
                items[j] = items[j + 1];
                items[j + 1] = temp;
                j--;
            }
        }
    }

    private static void UpdateValues(Dictionary<string, Item> itemsById, string[] tokens)
    {
        var name = tokens[1];
        var x = int.Parse(tokens[2]);
        var y = int.Parse(tokens[3]);

        var item = itemsById[name];

        item.X1 = x;
        item.Y1 = y;
    }

    private static void AddItem(List<Item> items, Dictionary<string, Item> itemsById, string[] tokens)
    {
        var name = tokens[1];
        var x = int.Parse(tokens[2]);
        var y = int.Parse(tokens[3]);

        var item = new Item(x, y, name);

        items.Add(item);
        itemsById[name] = item;
    }
}