namespace CountOfOccurrences
{
    using ReversedList;
    using System;

    public class Program
    {
        public static void Main()
        {
            ReversedList<int> reversedList = new ReversedList<int>();
            reversedList.Add(100);
            reversedList.Add(10);
            reversedList.Add(1);

            foreach (var number in reversedList)
            {
                Console.WriteLine(number);
            }
        }
    }
}