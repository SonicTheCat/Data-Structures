public class Program
{
    public static void Main()
    {
        ArrayStack<int> arrayStack = new ArrayStack<int>();

        arrayStack.Push(10);
        arrayStack.Push(20);
        arrayStack.Push(30);

        //System.Console.WriteLine("Count: " + arrayStack.Count);

        //System.Console.WriteLine(arrayStack.Pop());
        //System.Console.WriteLine("Count: " + arrayStack.Count);
        //System.Console.WriteLine(arrayStack.Pop());
        //System.Console.WriteLine("Count: " + arrayStack.Count);

        //System.Console.WriteLine(arrayStack.Pop());
        //System.Console.WriteLine("Count: " + arrayStack.Count);
        //System.Console.WriteLine(arrayStack.Pop());

        arrayStack.Push(2);
        arrayStack.Push(3);

        var arr = arrayStack.ToArray();

        foreach (var item in arr)
        {
            System.Console.WriteLine(item);
        }
    }
}