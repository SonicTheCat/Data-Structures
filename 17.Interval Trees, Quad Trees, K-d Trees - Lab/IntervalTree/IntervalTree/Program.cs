public class Program
{
    public static void Main()
    {
        IntervalTree tree = new IntervalTree();
        tree.Insert(10, 55);
        tree.Insert(7, 45);
        tree.Insert(1, 3); 
        tree.Insert(9, 40); 
        tree.Insert(55, 70); 
        tree.Insert(25, 30); 
        tree.Insert(60, 65);

        //var result = tree.SearchAny(4, 6); 
        var result = tree.SearchAll(20, 57);
    }
}
