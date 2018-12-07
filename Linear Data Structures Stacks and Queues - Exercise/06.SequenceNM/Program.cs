using System;
using System.Collections.Generic;
using System.Linq;

public class Program
{
    public static void Main()
    {
        var input = Console.ReadLine()
            .Split()
            .Select(int.Parse)
            .ToArray();

        var n = input[0];
        var m = input[1];

        var elements = new Queue<Element>();
        elements.Enqueue(new Element(n, null));

        var result = new Stack<int>();

        while (elements.Count > 0)
        {
            var element = elements.Dequeue();

            if (element.Value == m)
            {
                while (element != null)
                {
                    result.Push(element.Value);
                    element = element.Prev;
                }
                Console.WriteLine(string.Join(" -> ", result));
                return;
            }

            if (element.Value < m)
            {
                elements.Enqueue(new Element(element.Value + 1, element));
                elements.Enqueue(new Element(element.Value + 2, element));
                elements.Enqueue(new Element(element.Value * 2, element));
            }
        }
    }

    public class Element
    {
        public Element(int value, Element prev)
        {
            this.Value = value;
            this.Prev = prev;
        }

        public int Value { get; private set; }

        public Element Prev { get; private set; }
    }
}