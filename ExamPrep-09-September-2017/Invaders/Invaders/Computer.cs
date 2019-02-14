using System;
using System.Collections.Generic;
using System.Linq;

public class Computer : IComputer
{
    private int energy;
    private readonly HashSet<Invader> byOrder;

    public Computer(int energy)
    {
        if (energy < 0)
        {
            throw new ArgumentException();
        }
        this.energy = energy;
        this.byOrder = new HashSet<Invader>();
    }

    public int Energy => Math.Max(0, this.energy);

    public void AddInvader(Invader invader)
    {
        this.byOrder.Add(invader);
    }

    public void Skip(int turns)
    {
        List<Invader> invadersToRemove = new List<Invader>();
        foreach (var invader in this.byOrder)
        {
            invader.Distance -= turns;
            if (invader.Distance <= 0)
            {
                invadersToRemove.Add(invader);
                this.energy -= invader.Damage;
            }
        }
        invadersToRemove.ForEach(x => byOrder.Remove(x)); 
    }

    public void DestroyHighestPriorityTargets(int count)
    {
        var invadersToRemove = this.byOrder
            .OrderBy(x => x.Distance)
            .ThenByDescending(x => x.Damage)
            .Take(count)
            .ToList();

        invadersToRemove.ForEach(x => byOrder.Remove(x));
    }

    public void DestroyTargetsInRadius(int radius)
    {
        var invadersToRemove =
            this.byOrder
            .Where(x => x.Distance <= radius)
            .ToList();

        invadersToRemove.ForEach(x => byOrder.Remove(x));
    }

    public IEnumerable<Invader> Invaders()
    {
        return this.byOrder; 
    }
}
