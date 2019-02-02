using System;
using System.Linq;

public class StartUp
{
    public static void Main()
    {
        CustomDictionary<char, int> dict = new CustomDictionary<char, int>();

        var input = Console.ReadLine();

        foreach (var letter in input)
        {
            if (!dict.ContainsKey(letter))
            {
                dict[letter] = 0;
            }

            dict[letter]++;
        }

        foreach (var kvp in dict.OrderBy(x => x.Key))
        {
            Console.WriteLine(kvp.Key + ": " + kvp.Value + " time/s");
        }
    }
}