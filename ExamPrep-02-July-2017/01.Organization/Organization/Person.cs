using System;
using System.Collections.Generic;

public class Person
{
    public Person(string name, double salary)
    {
        this.Name = name;
        this.Salary = salary;
    }

    public string Name { get; set; }
    public double Salary { get; set; }

    public override bool Equals(object obj)
    {
        if (!(obj is Person))
        {
            return false; 
        }

        var person = (Person)obj;

        if (this.Name != person.Name || this.Salary != person.Salary)
        {
            return false;
        }

        return true;
    }

    public override int GetHashCode()
    {
        var hashCode = -1423493799;
        hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
        hashCode = hashCode * -1521134295 + Salary.GetHashCode();
        return hashCode;
    }

    public override string ToString()
    {
        return $"Name: {this.Name}, Salary: {this.Salary}"; 
    }
}
