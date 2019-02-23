using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class RoyaleArena : IArena
{

    private readonly LinkedList<Battlecard> byInsertion;
    private readonly OrderedBag<LinkedListNode<Battlecard>> bySwag;
    private readonly Dictionary<int, LinkedListNode<Battlecard>> byId;
    private readonly Dictionary<string, OrderedSet<LinkedListNode<Battlecard>>> byName;
    private readonly Dictionary<CardType, OrderedSet<LinkedListNode<Battlecard>>> byType;
    
    public RoyaleArena()
    {
        this.byInsertion = new LinkedList<Battlecard>();
        this.byId = new Dictionary<int, LinkedListNode<Battlecard>>();
        this.byName = new Dictionary<string, OrderedSet<LinkedListNode<Battlecard>>>();
        this.bySwag = new OrderedBag<LinkedListNode<Battlecard>>((x, y) => x.Value.Swag.CompareTo(y.Value.Swag));
        this.byType = new Dictionary<CardType, OrderedSet<LinkedListNode<Battlecard>>>()
        {
            { CardType.BUILDING, new OrderedSet<LinkedListNode<Battlecard>>((x,y) => x.Value.CompareTo(y.Value)) },
            { CardType.MELEE, new OrderedSet<LinkedListNode<Battlecard>>((x,y) => x.Value.CompareTo(y.Value)) },
            { CardType.RANGED, new OrderedSet<LinkedListNode<Battlecard>>((x,y) => x.Value.CompareTo(y.Value)) },
            { CardType.SPELL, new OrderedSet<LinkedListNode<Battlecard>>((x,y) => x.Value.CompareTo(y.Value)) }
        };
    }

    public int Count => this.byId.Count;

    public void Add(Battlecard card)
    {
        if (this.byId.ContainsKey(card.Id))
        {
            return;
        }

        var id = card.Id;
        var name = card.Name;
        var type = card.Type;
        var node = new LinkedListNode<Battlecard>(card);

        if (!this.byName.ContainsKey(name))
        {
            this.byName.Add(name, new OrderedSet<LinkedListNode<Battlecard>>(new SwagComparator()));
        }

        this.byName[name].Add(node);
        this.byId.Add(id, node);
        this.byInsertion.AddLast(node);
        this.byType[type].Add(node);
        this.bySwag.Add(node);
    }

    public bool Contains(Battlecard card)
    {
        return this.byId.ContainsKey(card.Id);
    }

    public void ChangeCardType(int id, CardType type)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new ArgumentException();
        }

        this.byId[id].Value.Type = type;
    }

    public Battlecard GetById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        return this.byId[id].Value;
    }

    public void RemoveById(int id)
    {
        if (!this.byId.ContainsKey(id))
        {
            throw new InvalidOperationException();
        }

        var node = this.byId[id];
        this.byId.Remove(id);
        this.byInsertion.Remove(node);
        this.byType[node.Value.Type].Remove(node);
        this.byName[node.Value.Name].Remove(node);
        this.bySwag.Remove(node);
    }

    public IEnumerable<Battlecard> GetByCardType(CardType type)
    {
        if (!this.byType[type].Any())
        {
            throw new InvalidOperationException();
        }

        return this.byType[type].Select(x => x.Value);
    }

    public IEnumerable<Battlecard> GetByCardTypeAndMaximumDamage(CardType type, double damage)
    {
        if (!this.byType[type].Any())
        {
            throw new InvalidOperationException();
        }

        var card = new Battlecard(int.MinValue, type, "", damage, 0);
        var node = new LinkedListNode<Battlecard>(card);

        return this.byType[type].RangeFrom(node, true).Select(x => x.Value);
    }

    public IEnumerable<Battlecard> GetByTypeAndDamageRangeOrderedByDamageThenById(CardType type, int lo, int hi)
    {
        if (!this.byType[type].Any())
        {
            throw new InvalidOperationException();
        }

        var low = new Battlecard(int.MinValue, type, "", lo, 0);
        var high = new Battlecard(int.MinValue, type, "", hi, 0);
        var node1 = new LinkedListNode<Battlecard>(low);
        var node2 = new LinkedListNode<Battlecard>(high);

        return this.byType[type].Range(node2, false, node1, false).Select(x => x.Value);
    }

    public IEnumerable<Battlecard> GetByNameOrderedBySwagDescending(string name)
    {
        if (!this.byName.ContainsKey(name))
        {
            throw new InvalidOperationException();
        }

        return this.byName[name].Select(x => x.Value);
    }

    public IEnumerable<Battlecard> GetByNameAndSwagRange(string name, double lo, double hi)
    {
        if (!this.byName.ContainsKey(name))
        {
            throw new InvalidOperationException();
        }

        var low = new Battlecard(int.MinValue, CardType.MELEE, "", 1, lo);
        var high = new Battlecard(int.MinValue, CardType.MELEE, "", 1, hi);
        var node1 = new LinkedListNode<Battlecard>(low);
        var node2 = new LinkedListNode<Battlecard>(high);

        return this.byName[name].Range(node2, false, node1, true).Select(x => x.Value);
    }

    public IEnumerable<Battlecard> GetAllByNameAndSwag()
    {
        var list = new List<Battlecard>();
        foreach (var kvp in this.byName)
        {
            yield return kvp.Value.Select(x => x.Value).ToArray()[0];
        }
    }

    public IEnumerable<Battlecard> GetAllInSwagRange(double lo, double hi)
    {
        var low = new Battlecard(int.MinValue, CardType.MELEE, "", 1, lo);
        var high = new Battlecard(int.MinValue, CardType.MELEE, "", 1, hi);
        var node1 = new LinkedListNode<Battlecard>(low);
        var node2 = new LinkedListNode<Battlecard>(high);

        return this.bySwag.Range(node1, true, node2, true).Select(x => x.Value);
    }

    public IEnumerable<Battlecard> FindFirstLeastSwag(int n)
    {
        if (n < 0 || n >= this.Count)
        {
            throw new InvalidOperationException();
        }

        return this.bySwag.Take(n).Select(x => x.Value).OrderBy(x => x.Swag).ThenBy(x => x.Id);
    }

    public IEnumerator<Battlecard> GetEnumerator()
    {
        foreach (var card in this.byInsertion)
        {
            yield return card;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}