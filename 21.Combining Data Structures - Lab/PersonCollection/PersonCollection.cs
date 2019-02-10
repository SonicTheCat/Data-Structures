using System;
using System.Collections.Generic;
using Wintellect.PowerCollections;

public class PersonCollection : IPersonCollection
{
    private readonly Dictionary<string, Person> personsByEmail;
    private readonly Dictionary<string, SortedSet<Person>> personsByDomain;
    private readonly Dictionary<string, SortedSet<Person>> personsByNameTown;
    private readonly OrderedDictionary<int, SortedSet<Person>> personsByAge;
    private readonly Dictionary<string, OrderedDictionary<int, SortedSet<Person>>> personsByTownAndAge;

    public PersonCollection()
    {
        this.personsByEmail = new Dictionary<string, Person>();
        this.personsByDomain = new Dictionary<string, SortedSet<Person>>();
        this.personsByNameTown = new Dictionary<string, SortedSet<Person>>();
        this.personsByAge = new OrderedDictionary<int, SortedSet<Person>>();
        this.personsByTownAndAge = new Dictionary<string, OrderedDictionary<int, SortedSet<Person>>>();
    }

    public int Count => this.personsByEmail.Count;

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.personsByEmail.ContainsKey(email))
        {
            return false;
        }

        var person = new Person(email, name, age, town);

        this.personsByEmail.Add(email, person);

        var domain = person.Email.Split('@')[1];
        this.personsByDomain.AppendValueToKey(domain, person);

        var townAndName = this.CombineTownAndName(name, town); 
        this.personsByNameTown.AppendValueToKey(townAndName, person);

        this.personsByAge.AppendValueToKey(age, person);

        this.personsByTownAndAge.EnsureKeyExists(town);
        this.personsByTownAndAge[town].AppendValueToKey(age, person); 

        return true;
    }

    public Person FindPerson(string email)
    {
        if (this.personsByEmail.ContainsKey(email))
        {
            return this.personsByEmail[email];
        }

        return null;
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        if (person == null)
        {
            return false;
        }

        this.personsByEmail.Remove(email);

        var domain = person.Email.Split('@')[1];
        this.personsByDomain[domain].Remove(person);

        var nameAndTown = this.CombineTownAndName(person.Name, person.Town);
        this.personsByNameTown[nameAndTown].Remove(person);

        var age = person.Age;
        this.personsByAge[age].Remove(person);

        var town = person.Town;
        this.personsByTownAndAge[town][age].Remove(person);

        return true;
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.personsByDomain.GetValuesForKey(emailDomain);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        var key = this.CombineTownAndName(name, town); 
        return this.personsByNameTown.GetValuesForKey(key); 
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        var personsInRange = this.personsByAge.Range(startAge, true, endAge, true);
        foreach (var kvp in personsInRange)
        {
            foreach (var person in kvp.Value)
            {
                yield return person;
            }
        }
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        if (!this.personsByTownAndAge.ContainsKey(town))
        {
            yield break; 
        }

        var personsInRange = this.personsByTownAndAge[town].Range(startAge, true, endAge, true);

        foreach (var kvp in personsInRange)
        {
            foreach (var person in kvp.Value)
            {
                yield return person;
            }
        }
    }

    private string CombineTownAndName(string name, string town)
    {
        const string separator = "!";
        return name + separator + town; 
    }
}