using System;
using System.Collections.Generic;
using System.Linq;

namespace RemoveOddOccurrences
{
    public class StartUp
    {
        public static void Main()
        {
            var numbers = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

            var result = new List<int>();

            for (int i = 0; i < numbers.Length; i++)
            {
                var currentNumber = numbers[i];
                var occurCounter = 0;

                for (int j = 0; j < numbers.Length; j++)
                {
                    if (numbers[j] == currentNumber)
                    {
                        occurCounter++;
                    }
                }

                if (occurCounter % 2 == 0)
                {
                    result.Add(currentNumber);
                }
            }
            Console.WriteLine(string.Join(" ", result));
        }
    }
}