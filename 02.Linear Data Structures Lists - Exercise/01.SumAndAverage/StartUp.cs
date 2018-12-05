using System;
using System.Linq;

namespace SumAndAverage
{
    class StartUp
    {
        static void Main()
        {
            var sequenceInput = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToList();

            var sum = 0;
           
            foreach (var element in sequenceInput)
            {
                sum += element;
            }

            double average = (double)sum / sequenceInput.Count;

            Console.WriteLine("Sum=" + sum + "; Average=" + average.ToString("F2"));
        }
    }
}