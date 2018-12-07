using System;
using System.Collections.Generic;
using System.Linq;

namespace ReverseNumbers
{
    public class Program
    {
        public static void Main()
        {
            var input = Console.ReadLine()
                .Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse);

            Stack<int> stack = new Stack<int>(input);

            Console.WriteLine(string.Join(" ", stack));
        }
    }
}