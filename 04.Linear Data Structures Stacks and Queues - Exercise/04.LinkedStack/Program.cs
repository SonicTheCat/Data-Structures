public class Program
{
    public static void Main()
    {
        LinkedStack<int> linked = new LinkedStack<int>();

        linked.Push(10);
        linked.Push(33);
        linked.Push(200);

        var arr = linked.ToArray();

        foreach (var item in arr)
        {
            System.Console.WriteLine(item);
        }
    }
}