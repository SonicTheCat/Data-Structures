using System;
using System.Collections.Generic;
using System.Linq;

public class PersonCollectionSlow : IPersonCollection
{
    private List<Person> people;

    public PersonCollectionSlow()
    {
        this.people = new List<Person>(); 
    }

    public bool AddPerson(string email, string name, int age, string town)
    {
        if (this.FindPerson(email) != null)
        {
            return false;
        }

        this.people.Add(new Person(email, name, age, town));
        return true; 
    }

    public int Count => this.people.Count; 

    public Person FindPerson(string email)
    {
       return this.people.FirstOrDefault(x => x.Email == email);
    }

    public bool DeletePerson(string email)
    {
        var person = this.FindPerson(email);
        return this.people.Remove(person); 
    }

    public IEnumerable<Person> FindPersons(string emailDomain)
    {
        return this.people
            .Where(x => x.Email.EndsWith(emailDomain))
            .OrderBy(x => x.Email);
    }

    public IEnumerable<Person> FindPersons(string name, string town)
    {
        return this.people
            .Where(x => x.Name == name && x.Town == town)
            .OrderBy(x => x.Email); ;
    }

    public IEnumerable<Person> FindPersons(int startAge, int endAge)
    {
        return this.people
             .Where(x => x.Age >= startAge && x.Age <= endAge)
             .OrderBy(x => x.Age).ThenBy(x => x.Email); 
    }

    public IEnumerable<Person> FindPersons(
        int startAge, int endAge, string town)
    {
        return this.people
                .Where(x => x.Town == town)
               .Where(x => x.Age >= startAge && x.Age <= endAge)
               .OrderBy(x => x.Age).ThenBy(x => x.Email);
    }
}
