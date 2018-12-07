using System;
using System.Collections.Generic;

namespace CalculateSequence
{
    public class Program
    {
        public static void Main()
        {
            var n = int.Parse(Console.ReadLine());

            var queue = new Queue<int>(new int[] { n });
            var helperQue = new Queue<int>();

            for (int i = 0; i < 50; i++)
            {
                var current = queue.Dequeue();

                queue.Enqueue(current + 1);
                queue.Enqueue(2 * current + 1);
                queue.Enqueue(current + 2);

                helperQue.Enqueue(current);
            }

            Console.WriteLine(string.Join(", ", helperQue));
        }
    }
}