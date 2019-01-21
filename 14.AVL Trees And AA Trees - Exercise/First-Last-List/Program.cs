using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wintellect.PowerCollections;

public class Program
{
    static void Main(string[] args)
    {
        FirstLastList<int> list = new FirstLastList<int>();
        //list.Add(1);
        //list.Add(2);
        //list.Add(3);
        //list.Add(4);
        //Console.WriteLine();

        list.Add(200);
        list.Add(120);
        list.Add(220);
        list.Add(220);
        list.Add(10);
        list.Add(6);
        list.Add(1);

        list.RemoveAll(220);
        var result = list.Max(4);

        Console.WriteLine();
    }
}
