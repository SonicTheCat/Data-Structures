using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

/// <summary>
/// The ThreadExecutor is the concrete implementation of the IScheduler.
/// You can send any class to the judge system as long as it implements
/// the IScheduler interface. The Tests do not contain any <e>Reflection</e>!
/// </summary>
public class ThreadExecutor : IScheduler
{
    private int totalCycles;

    private Dictionary<int, LinkedListNode<Task>> byId = new Dictionary<int, LinkedListNode<Task>>();
    private List<LinkedListNode<Task>> byIndex = new List<LinkedListNode<Task>>();
    private OrderedBag<LinkedListNode<Task>> byConsumption = new OrderedBag<LinkedListNode<Task>>((x, y) => x.Value.CompareTo(y.Value)); 
    private Dictionary<Priority, OrderedSet<LinkedListNode<Task>>> byPriority = new Dictionary<Priority, OrderedSet<LinkedListNode<Task>>>();

    public int Count => this.byId.Count;

    public void Execute(Task task)
    {
        if (this.byId.ContainsKey(task.Id))
        {
            throw new ArgumentException();
        }

        var node = new LinkedListNode<Task>(task);
        var consumption = node.Value.Consumption;
        var priority = node.Value.TaskPriority;

       
        if (!this.byPriority.ContainsKey(priority))
        {
            this.byPriority.Add(priority, new OrderedSet<LinkedListNode<Task>>((x, y) => -x.Value.Id.CompareTo(y.Value.Id)));
        }

        this.byPriority[priority].Add(node);
        this.byConsumption.Add(node);
        this.byId.Add(task.Id, node);
        this.byIndex.Add(node);
    }

    public bool Contains(Task task)
    {
        return this.byId.ContainsKey(task.Id);
    }

    public Task GetById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        return this.byId[id].Value;
    }

    public Task GetByIndex(int index)
    {
        if (index < 0 || index >= this.byIndex.Count)
        {
            throw new ArgumentOutOfRangeException();
        }

        return this.byIndex[index].Value;
    }

    public int Cycle(int cycles)
    {
        if (this.byIndex.Count == 0)
        {
            throw new InvalidOperationException();
        }

        this.totalCycles += cycles;
        var removedCount = 0;

        for (int i = this.byIndex.Count - 1; i >= 0; i--)
        {
            var node = this.byIndex[i];
            var id = node.Value.Id;
            if (node.Value.Consumption - totalCycles <= 0)
            {
                this.byIndex.Remove(node);
                this.byId.Remove(id);
                this.byConsumption.Remove(node);
                this.byPriority[node.Value.TaskPriority].Remove(node);
                removedCount++;
            }
        }

        return removedCount;
    }

    public IEnumerable<Task> GetByConsumptionRange(int lo, int hi, bool inclusive)
    {
        LinkedListNode<Task> low = new LinkedListNode<Task>(new Task(5, lo + this.totalCycles, inclusive ? Priority.EXTREME : Priority.LOW));
        LinkedListNode<Task> high = new LinkedListNode<Task>(new Task(7, hi + this.totalCycles, inclusive ? Priority.LOW : Priority.EXTREME));
        return this.byConsumption.Range(low, inclusive, high, inclusive).Select(x => x.Value);
    }

    public void ChangePriority(int id, Priority newPriority)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        var node = this.byId[id]; 
        this.byPriority[node.Value.TaskPriority].Remove(node);
        this.byId[id].Value.TaskPriority = newPriority;

        if (!this.byPriority.ContainsKey(newPriority))
        {
            this.byPriority.Add(newPriority, new OrderedSet<LinkedListNode<Task>>((x, y) => -x.Value.Id.CompareTo(y.Value.Id)));
        }

        this.byPriority[newPriority].Add(node);
    }

    public IEnumerable<Task> GetByPriority(Priority type)
    {
        var list = new List<Task>();
        if (this.byPriority.ContainsKey(type))
        {
            foreach (var node in this.byPriority[type])
            {
                list.Add(node.Value);
            }
        }

        return list;
    }

    public IEnumerable<Task> GetByPriorityAndMinimumConsumption(Priority priority, int lo)
    {
        var list = new List<Task>();
        if (this.byPriority.ContainsKey(priority))
        {
            foreach (var node in this.byPriority[priority].Where(x => x.Value.Consumption >= lo))
            {
                list.Add(node.Value);
            }
        }

        return list;
    }

    public IEnumerator<Task> GetEnumerator()
    {
        foreach (var node in this.byIndex)
        {
            yield return node.Value;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}
