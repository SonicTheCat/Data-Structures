using System.Collections.Generic;

public class SwagComparator : IComparer<LinkedListNode<Battlecard>>
{
    public int Compare(LinkedListNode<Battlecard> x, LinkedListNode<Battlecard> y)
    {
        var cmp = -x.Value.Swag.CompareTo(y.Value.Swag);

        if (cmp == 0)
        {
            cmp = x.Value.Id.CompareTo(y.Value.Id);
        }

        return cmp;
    }
}