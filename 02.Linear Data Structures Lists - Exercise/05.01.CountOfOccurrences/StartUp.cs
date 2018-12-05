using System;
using System.Collections.Generic;
using System.Linq;

namespace CountOfOccurrences
{
    class StartUp
    {
        static void Main()
        {
            var numbers = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

            var list = new int[1000];

            foreach (var number in numbers)
            {
                list[number]++;
            }

            for (int i = 0; i < list.Length; i++)
            {
                if (list[i] != 0)
                {
                    Console.WriteLine(i + " -> " + list[i] + " times");
                }
            }
        }
    }
}
