using System;
using System.Collections.Generic;
using System.Linq;

public class StartUp
{
    private static readonly Dictionary<int, Tree<int>> nodeByValue = new Dictionary<int, Tree<int>>();
    private static int deapestCount = 0;
    private static int longestPath = 0;

    public static void Main()
    {
        ReadTree();

        var root = GetRootNode();

        //Console.Clear(); 

        //Task 2
        // PrintTree(root, 0);

        //Task 3
        //var allLeafsOrdered = FindAllLeafNodes();
        //Console.WriteLine("Leaf nodes: " + string.Join(" ", allLeafsOrdered));

        //Task 4
        //var allMiddlesNodes = GetMiddleNodes(root);
        //Console.WriteLine("Middle nodes: " + string.Join(" ", allMiddlesNodes));

        //Task 5
        //var list = new Stack<int>();
        //var deepestValue = FindDeepestNode(root, list);
        //Console.WriteLine("Deepest node: " + deepestValue);

        //Task 5 - second solution 
        //var deepestValue = FindDeepestNode(root);
        //Console.WriteLine("Deepest node: " + deepestValue.Value);

        //Task 6
        //var Stack = new Stack<int>();
        //var longestPath = FindLongestPath(root, Stack);
        //Console.WriteLine("Longest path: " + longestPath);

        //Task 7
        //var targetSum = int.Parse(Console.ReadLine());
        //var queue = new Queue<string>();
        //FindAllPathsWithGivenSum(root, queue, targetSum, 0);
        //Console.WriteLine($"Paths of sum {targetSum}:\n" + string.Join("\n", queue));

        //Task 8
        //var targetSum = int.Parse(Console.ReadLine());
        //var queue = new Queue<string>();
        //FindSubTreeWithGivenSum(root, queue, targetSum);
        //Console.WriteLine($"Subtrees of sum {targetSum}:\n" + string.Join("\n", queue));

        //Task 8 - second solution
        foreach (var node in FindSubTreeWithGivenSum(root))
        {
            Print(node);
            Console.WriteLine();
        }
    }

    //Task 8 - second solution
    public static void Print(Tree<int> node)
    {
        Console.Write(node.Value + " ");
        foreach (var child in node.Children)
        {
            Print(child); 
        }
    }

    //Task 8 - second solution 
    public static List<Tree<int>> FindSubTreeWithGivenSum(Tree<int> root)
    {
        var targetNumber = int.Parse(Console.ReadLine());
        var roots = new List<Tree<int>>();

        var totalSum = DFS(root, targetNumber, roots);

        Console.WriteLine($"Subtrees of sum {targetNumber}:");
        return roots;
    }

    //Task 8 - second solution
    public static int DFS(Tree<int> node, int targetSum, List<Tree<int>> roots)
    {
        if (node == null)
        {
            return 0; 
        }

        var currentSum = node.Value; 

        foreach (var child in node.Children)
        {
          currentSum +=  DFS(child, targetSum,roots); 
        }

        if (currentSum == targetSum)
        {
            roots.Add(node); 
        }

        return currentSum; 
    }

    ////Task 8
    //public static void FindSubTreeWithGivenSum(Tree<int> node, Queue<string> queue, int targetSum)
    //{
    //    if (node == null)
    //    {
    //        return;
    //    }
        
    //    List<int> list = new List<int>();
    //    AddTreeToList(node, list);

    //    if (list.Sum() == targetSum)
    //    {
    //        queue.Enqueue(string.Join(" ", list));
    //    }

    //    foreach (var child in node.Children)
    //    {
    //        FindSubTreeWithGivenSum(child, queue, targetSum);
    //    }
    //}

    ////Task 8
    //public static void AddTreeToList(Tree<int> node, List<int> list)
    //{
    //    list.Add(node.Value);

    //    foreach (var child in node.Children)
    //    {
    //        AddTreeToList(child, list);
    //    }
    //}

    //Task 7
    public static void FindAllPathsWithGivenSum(Tree<int> node, Queue<string> queue, int targetSum, int currentSum)
    {
        if (node == null)
        {
            return;
        }

        currentSum += node.Value;

        if (currentSum == targetSum && node.Children.Count == 0)
        {
            AddSequence(node, queue);
        }

        foreach (var child in node.Children)
        {
            FindAllPathsWithGivenSum(child, queue, targetSum, currentSum);
        }
    }

    //Task 7
    public static void AddSequence(Tree<int> node, Queue<string> queue)
    {
        var result = new Stack<int>();
        while (node != null)
        {
            result.Push(node.Value);
            node = node.Parent;
        }

        queue.Enqueue(string.Join(" ", result));
    }

    //Task 6
    public static string FindLongestPath(Tree<int> node, Stack<int> longestSequence, int currentLongest = 0)
    {
        if (node.Children.Count == 0 && longestPath < currentLongest)
        {
            longestPath = currentLongest;
            GetSequence(node, longestSequence);
        }

        foreach (var child in node.Children)
        {
            FindLongestPath(child, longestSequence, currentLongest + 1);
        }

        return string.Join(" ", longestSequence);
    }

    //Task 6
    public static void GetSequence(Tree<int> node, Stack<int> longestSeq)
    {
        longestSeq.Clear();

        while (node != null)
        {
            longestSeq.Push(node.Value);
            node = node.Parent;
        }
    }

    //Task 5 - second solution 
    public static Tree<int> FindDeepestNode(Tree<int> root)
    {
        var queue = new Queue<Tree<int>>();
        queue.Enqueue(root);
        Tree<int> current = null;

        while (queue.Count > 0)
        {
            current = queue.Dequeue();

            for (int i = current.Children.Count - 1; i >= 0; i--)
            {
                queue.Enqueue(current.Children[i]);
            }
        }

        return current;
    }

    ////Task 5
    //public static int FindDeepestNode(Tree<int> node, Stack<int> stack, int currentDept = 1)
    //{
    //    if (deapestCount < currentDept)
    //    {
    //        stack.Push(node.Value);
    //        deapestCount = currentDept;
    //    }

    //    foreach (var child in node.Children)
    //    {
    //        FindDeepestNode(child, stack, currentDept + 1);
    //    }

    //    return stack.Peek();
    //}

    //Task 4
    public static IEnumerable<int> GetMiddleNodes(Tree<int> root)
    {
        var list = new List<int>();

        AddMiddleNodesToList(root, list);

        return list.OrderBy(x => x).ToList();
    }

    //Task 4
    public static void AddMiddleNodesToList(Tree<int> node, List<int> list)
    {
        if (node.Parent != null && node.Children.Count != 0)
        {
            list.Add(node.Value);
        }

        foreach (var child in node.Children)
        {
            AddMiddleNodesToList(child, list);
        }
    }

    //Task 3
    public static IEnumerable<int> GetLeafNodes(Tree<int> root)
    {
        var list = new List<int>();

        AddLeafNodesToList(root, list);

        return list.OrderBy(x => x).ToList();
    }

    //Task 3
    public static void AddLeafNodesToList(Tree<int> node, List<int> list)
    {
        if (node.Children.Count == 0)
        {
            list.Add(node.Value);
            return;
        }

        foreach (var child in node.Children)
        {
            AddLeafNodesToList(child, list);
        }
    }

    //Task 2
    public static void PrintTree(Tree<int> node, int spaces = 0)
    {
        Console.WriteLine(new string(' ', spaces) + node.Value);

        foreach (var child in node.Children)
        {
            PrintTree(child, spaces + 2);
        }
    }

    //Task 1
    public static Tree<int> GetTreeNodeByValue(int value)
    {
        if (!nodeByValue.ContainsKey(value))
        {
            nodeByValue[value] = new Tree<int>(value);
        }

        return nodeByValue[value];
    }

    //Task 1
    public static void AddEdge(int parent, int child)
    {
        Tree<int> parentNode = GetTreeNodeByValue(parent);
        Tree<int> childNode = GetTreeNodeByValue(child);

        parentNode.Children.Add(childNode);
        childNode.Parent = parentNode;
    }

    //Task 1
    public static void ReadTree()
    {
        int nodeCount = int.Parse(Console.ReadLine());

        for (int i = 1; i < nodeCount; i++)
        {
            var edge = Console.ReadLine()
                .Split()
                .Select(int.Parse)
                .ToArray();

            AddEdge(edge[0], edge[1]);
        }
    }

    //Task 1
    public static Tree<int> GetRootNode()
    {
        return nodeByValue.Values.FirstOrDefault(x => x.Parent == null);
    }
}