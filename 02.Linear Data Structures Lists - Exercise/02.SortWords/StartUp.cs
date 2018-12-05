using System;
using System.Linq;

namespace SortWords
{
    public class StartUp
    {
        public static void Main()
        {
            var sequenceInput = Console.ReadLine()
                .Split()
                .ToList<string>();

            sequenceInput.Sort();

            Console.WriteLine(string.Join(" ", sequenceInput));
        }
    }
}