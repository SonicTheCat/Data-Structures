using System;
using System.Collections.Generic;
using System.Linq;

public class Tree<T>
{
    public Tree(T value, params Tree<T>[] children)
    {
        this.Value = value;
        this.Children = children.ToList();
    }

    public T Value { get; private set; }

    public List<Tree<T>> Children { get; }

    public void Print(int indent = 0)
    {
        Console.WriteLine(new string(' ', indent) + this.Value);

        foreach (var child in this.Children)
        {
            child.Print(indent + 2);
        }
    }

    public void Each(Action<T> action)
    {
        action(this.Value);

        foreach (var child in this.Children)
        {
            child.Each(action);
        }
    }

    public IEnumerable<T> OrderDFS()
    {
        var result = new List<T>();
        this.DFS(this, result);
        return result;

        // --- Stack solution below: 

        //var collection = new Stack<Tree<T>>();
        //var result = new Stack<T>(); 

        //collection.Push(this);

        //while (collection.Count > 0)
        //{
        //    var element = collection.Pop();

        //    foreach (var child in element.Children)
        //    {
        //        collection.Push(child); 
        //    }

        //    result.Push(element.Value); 
        //}

        //return result; 
    }

    private void DFS(Tree<T> currentNode, List<T> result)
    {
        foreach (var child in currentNode.Children)
        {
            child.DFS(child, result);
        }

        result.Add(currentNode.Value);
    }

    public IEnumerable<T> OrderBFS()
    {
        var que = new Queue<Tree<T>>();
        var result = new List<T>();

        que.Enqueue(this);

        while (que.Count > 0)
        {
            var currentNode = que.Dequeue();

            foreach (var child in currentNode.Children)
            {
                que.Enqueue(child); 
            }

            result.Add(currentNode.Value); 
        }

        return result; 
    }
}
