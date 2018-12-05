using System;
using System.Collections.Generic;
using System.Linq;

namespace LongestSubsequence
{
    public class StartUp
    {
        public static void Main()
        {
            var bestSequence = FindBestSequenceOfIntegers();
            Console.WriteLine(string.Join(" ", bestSequence));
        }

        public static List<int> FindBestSequenceOfIntegers()
        {
            var sequenceInput = Console.ReadLine()
             .Split()
             .Select(int.Parse)
             .ToList();

            var currentBest = 1;
            var finalBest = 1;

            int currentNumber = sequenceInput[0];
            int finalNumber = sequenceInput[0];

            for (int i = 1; i < sequenceInput.Count; i++)
            {
                if (sequenceInput[i] == sequenceInput[i - 1])
                {
                    currentBest++;
                    currentNumber = sequenceInput[i];

                    if (i != sequenceInput.Count - 1)
                    {
                        continue;
                    }

                }

                if (currentBest > finalBest)
                {
                    finalBest = currentBest;
                    finalNumber = currentNumber;
                }

                currentBest = 1;
            }
            return Enumerable.Repeat(finalNumber, finalBest).ToList();
        }
    }
}