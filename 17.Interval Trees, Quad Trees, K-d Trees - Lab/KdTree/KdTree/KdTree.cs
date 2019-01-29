using System;

public class KdTree
{
    private Node root;

    public class Node
    {
        public Node(Point2D point)
        {
            this.Point = point;
        }

        public Point2D Point { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
    }

    public Node Root
    {
        get
        {
            return this.root;
        }
    }

    public bool Contains(Point2D point)
    {
        var result = this.Contains(this.root, point, 0);
        return result != null; 
    }

    private Node Contains(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return null;
        }

        var cmp = node.Point.CompareTo(point); 

        if (cmp > 0)
        {
            this.Contains(node.Left, point, depth + 1);
        }
        else if (cmp < 0)
        {
          this.Contains(node.Right, point, depth + 1);
        }

        return node;
    }

    public void Insert(Point2D point)
    {
        this.root = this.Insert(this.root, point, 0); 
    }

    private Node Insert(Node node, Point2D point, int depth)
    {
        if (node == null)
        {
            return new Node(point); 
        }

        var cmp = ComparePoints(node.Point, point, depth);

        if (cmp > 0)
        {
            node.Left = this.Insert(node.Left, point, depth + 1); 
        }
        else if (cmp < 0)
        {
            node.Right = this.Insert(node.Right, point, depth + 1); 
        }

        return node; 
    }

    private int ComparePoints(Point2D current, Point2D pointToInsert, int depth)
    {
        var result = 0;

        if (depth % 2 == 0)
        {
            result = current.X.CompareTo(pointToInsert.X); 
            if(result == 0)
            {
                result = current.Y.CompareTo(pointToInsert.Y); 
            }
        }
        else
        {
            result = current.Y.CompareTo(pointToInsert.Y);
            if (result == 0)
            {
                result = current.X.CompareTo(pointToInsert.Y); 
            }
        }

        return result;
    }

    public void EachInOrder(Action<Point2D> action)
    {
        this.EachInOrder(this.root, action);
    }

    private void EachInOrder(Node node, Action<Point2D> action)
    {
        if (node == null)
        {
            return;
        }

        this.EachInOrder(node.Left, action);
        action(node.Point);
        this.EachInOrder(node.Right, action);
    }
}
