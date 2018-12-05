using System;
using System.Linq;

namespace CountOfOccurrences
{
    class StartUp
    {
        static void Main(string[] args)
        {
            var numbers = Console.ReadLine()
          .Split()
          .Select(int.Parse)
          .OrderBy(n => n)
          .ToArray();


            for (int i = 0; i < numbers.Length; i++)
            {
                var currentNumber = numbers[i];
                var occurCounter = 1;

                for (int j = i + 1; j < numbers.Length; j++)
                {
                    if (numbers[j] != currentNumber)
                    {
                        break;
                    }

                    occurCounter++;
                }

                Console.WriteLine(currentNumber + " -> " + occurCounter + " times");

                i += occurCounter - 1; 
            }
        }
    }
}