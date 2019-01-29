using System;

public class Program
{
    static void Main(string[] args)
    {
        KdTree tree = new KdTree();
        tree.Insert(new Point2D(6, 8));
        tree.Insert(new Point2D(7, 5));
        tree.Insert(new Point2D(4, 6));
        tree.Insert(new Point2D(3, 3));
        tree.Insert(new Point2D(7, 9));
        Console.WriteLine();

        //root - 6; 8 
            // right child - 7; 5
                // right child - 7; 9
                // left child - null
            // left child - 4; 6
                // left child - 3; 3 
                // right child - null
           
    }
}
