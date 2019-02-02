using System;

public class Program
{
    public static void Main()
    {
        var dict = new CustomDictionary<string, string>();

        string input;
        while ((input = Console.ReadLine()) != "search")
        {
            var tokens = input.Split('-');
            var name = tokens[0];
            var number = tokens[1];

            dict[name] = number;
        }

        while ((input = Console.ReadLine()) != "end")
        {
            if (dict.TryGetValue(input, out string value))
            {
                Console.WriteLine(input + " -> " + value);
            }
            else
            {
                Console.WriteLine($"Contact {input} does not exist.");
            }
        }
    }
}