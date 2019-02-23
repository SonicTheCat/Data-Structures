using System;
using System.Collections;
using System.Collections.Generic;
using Wintellect.PowerCollections;
using System.Linq;

public class Instock : IProductStock
{
    private readonly List<Product> products;
    private readonly OrderedBag<Product> byPrice;
    private readonly OrderedSet<Product> byAlphabetical;
    private readonly Dictionary<string, LinkedListNode<Product>> byLabel;
    private readonly Dictionary<int, LinkedList<LinkedListNode<Product>>> byQuantity;

    public Instock()
    {
        this.products = new List<Product>();
        this.byAlphabetical = new OrderedSet<Product>();
        this.byLabel = new Dictionary<string, LinkedListNode<Product>>();
        this.byQuantity = new Dictionary<int, LinkedList<LinkedListNode<Product>>>();
        this.byPrice = new OrderedBag<Product>((x, y) => -x.Price.CompareTo(y.Price));
    }

    public int Count => this.products.Count;

    public void Add(Product product)
    {
        if (this.byLabel.ContainsKey(product.Label))
        {
            return;
        }

        this.byPrice.Add(product);
        this.products.Add(product);
        this.byAlphabetical.Add(product);

        if (!this.byQuantity.ContainsKey(product.Quantity))
        {
            this.byQuantity.Add(product.Quantity, new LinkedList<LinkedListNode<Product>>());
        }

        var node = new LinkedListNode<Product>(product);

        this.byLabel[product.Label] = node;
        this.byQuantity[product.Quantity].AddLast(node);
    }

    public void ChangeQuantity(string product, int quantity)
    {
        if (!this.byLabel.ContainsKey(product))
        {
            throw new ArgumentException();
        }

        var node = this.byLabel[product];
        this.byQuantity[node.Value.Quantity].Remove(node);
        this.byLabel[product].Value.Quantity = quantity;

        if (!this.byQuantity.ContainsKey(quantity))
        {
            this.byQuantity.Add(quantity, new LinkedList<LinkedListNode<Product>>());
        }

        this.byQuantity[quantity].AddLast(node);
    }

    public bool Contains(Product product)
    {
        if (this.byLabel.ContainsKey(product.Label))
        {
            return true;
        }
        return false;
    }

    public Product Find(int index)
    {
        if (index >= this.products.Count || index < 0)
        {
            throw new IndexOutOfRangeException();
        }

        return this.products[index];
    }

    public IEnumerable<Product> FindAllByPrice(double price)
    {
        foreach (var product in this.byPrice.Where(x => x.Price == price))
        {
            yield return product;
        }
    }

    public IEnumerable<Product> FindAllByQuantity(int quantity)
    {
        var collection = new List<Product>();
        if (this.byQuantity.ContainsKey(quantity))
        {
            foreach (var item in this.byQuantity[quantity])
            {
                collection.Add(item.Value);
            }
        }

        return collection;
    }

    public IEnumerable<Product> FindAllInRange(double lo, double hi)
    {
        return this.byPrice.Where(x => x.Price > lo && x.Price <= hi);
    }

    public Product FindByLabel(string label)
    {
        if (!this.byLabel.ContainsKey(label))
        {
            throw new ArgumentException();
        }

        return this.byLabel[label].Value;
    }

    public IEnumerable<Product> FindFirstByAlphabeticalOrder(int count)
    {
        if (count > this.Count)
        {
            throw new ArgumentException();
        }

        return this.byAlphabetical.Take(count);
    }

    public IEnumerable<Product> FindFirstMostExpensiveProducts(int count)
    {
        if (count > this.Count)
        {
            throw new ArgumentException();
        }

        return this.byPrice.Take(count);
    }

    public IEnumerator<Product> GetEnumerator()
    {
        foreach (var item in this.products)
        {
            yield return item;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();
}