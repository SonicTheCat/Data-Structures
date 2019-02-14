using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Wintellect.PowerCollections;

public class Organization : IOrganization
{
    private readonly List<Person> byIndex;
    private readonly Dictionary<string, LinkedList<Person>> byName;
    private readonly OrderedDictionary<int, LinkedList<Person>> bySizeOfName;

    public Organization()
    {
        this.byIndex = new List<Person>();
        this.byName = new Dictionary<string, LinkedList<Person>>();
        this.bySizeOfName = new OrderedDictionary<int, LinkedList<Person>>();
    }

    public int Count => this.byIndex.Count;

    public void Add(Person person)
    {
        var name = person.Name;

        this.byIndex.Add(person);
        this.byName.AppendValueToKey(name, person);
        this.bySizeOfName.AppendValueToKey(name.Length, person);
    }

    public bool Contains(Person person)
    {
        var valuesForName = this.byName.GetValuesForKey(person.Name);

        foreach (var pers in valuesForName)
        {
            if (pers.Equals(person))
            {
                return true;
            }
        }
        return false;
    }

    public bool ContainsByName(string name)
    {
        return this.byName.ContainsKey(name);
    }

    public Person GetAtIndex(int index)
    {
        if (index < 0 || index >= this.byIndex.Count)
        {
            throw new IndexOutOfRangeException();
        }

        return this.byIndex[index];
    }

    public IEnumerable<Person> GetByName(string name)
    {
        return this.byName.GetValuesForKey(name);
    }

    public IEnumerable<Person> FirstByInsertOrder(int count = 1)
    {
        return this.byIndex.Take(count);
    }

    public IEnumerable<Person> SearchWithNameSize(int minLength, int maxLength)
    {
        var people = this.bySizeOfName.Range(minLength, true, maxLength, true);

        foreach (var kvp in people)
        {
            foreach (var person in kvp.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> GetWithNameSize(int length)
    {
        return this.bySizeOfName.GetValuesForKey(length);
    }

    public IEnumerable<Person> PeopleByInsertOrder()
    {
        return this.byIndex; 
    }

    public IEnumerator<Person> GetEnumerator()
    {
        foreach (var person in this.byIndex)
        {
            yield return person;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}