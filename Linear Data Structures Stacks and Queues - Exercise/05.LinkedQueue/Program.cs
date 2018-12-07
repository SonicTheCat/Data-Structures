public class Program
{
    public static void Main()
    {
        var linkedQue = new LinkedQueue<int>();

        linkedQue.Enqueue(10);
        linkedQue.Enqueue(20);
        linkedQue.Enqueue(30);

        //System.Console.WriteLine("Count: " + linkedQue.Count);

        //linkedQue.Dequeue();
        //linkedQue.Dequeue();
        //linkedQue.Dequeue();

        System.Console.WriteLine("Count: " + linkedQue.Count);

        var arr = linkedQue.ToArray();

        System.Console.WriteLine("------------------------------");
        foreach (var item in arr)
        {
            System.Console.WriteLine(item);
        }
    }
}