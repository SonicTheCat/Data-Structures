using System;

public class PlayWithTrees
{
    static void Main()
    {

        //Tree<int> tree = new Tree<int>(1,
        //    new Tree<int>(2, new Tree<int>(22)),
        //    new Tree<int>(3));


        //var tree =
        //    new Tree<int>(7,
        //        new Tree<int>(19,
        //            new Tree<int>(1),
        //            new Tree<int>(12),
        //            new Tree<int>(31)),
        //        new Tree<int>(21),
        //        new Tree<int>(14,
        //            new Tree<int>(23),
        //            new Tree<int>(6)));

        //var result = tree.OrderBFS();

        //Console.WriteLine("Tree (indented):");
        //tree.Print();

        //Console.Write("Tree nodes:");
        //tree.Each(c => Console.Write(" " + c));
        //Console.WriteLine();

        //Console.WriteLine();

        //var binaryTree =
        //    new BinaryTree<int>(50,
        //         new BinaryTree<int>(10,
        //             new BinaryTree<int>(2)),
        //         new BinaryTree<int>(100,
        //            new BinaryTree<int>(75,
        //                new BinaryTree<int>(70)),
        //            new BinaryTree<int>(150)));

        //binaryTree.PrintIndentedPreOrder(); 

        var binaryTree =
            new BinaryTree<string>("*",
                new BinaryTree<string>("+",
                    new BinaryTree<string>("3"),
                    new BinaryTree<string>("2")),
                new BinaryTree<string>("-",
                    new BinaryTree<string>("9"),
                    new BinaryTree<string>("6")));

        //Console.WriteLine("Binary tree (indented, pre-order):");
        //binaryTree.PrintIndentedPreOrder();

        //Console.Write("Binary tree nodes (in-order):");
        //binaryTree.EachInOrder(c => Console.Write(" " + c));
        //Console.WriteLine();

        Console.Write("Binary tree nodes (post-order):");
        binaryTree.EachPostOrder(c => Console.Write(" " + c));
        Console.WriteLine();
    }
}
