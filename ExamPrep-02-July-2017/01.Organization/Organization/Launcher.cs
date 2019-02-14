using System.Diagnostics;

public class Launcher
{
    public static void Main()
    {
        //var person1 = new Person("Stamat", 1000); 
        //var person2 = new Person("Bai Ivan", 4040); 
        //var person3 = new Person("Stamat", 1000); 
        //var person4 = new Person("Stamat", 0.99); 

        //var org = new Organization();
        //org.Add(person1);
        //org.Add(person2);
        //org.Add(person3);
        //org.Add(person4);

        //org.Contains(person1);
        //org.Contains(new Person("Stamat", 1000));

        //org.ContainsByName("Stamat"); 
        //org.ContainsByName("Bai Ivan"); 
        //org.ContainsByName("Rachel"); 

        IOrganization org = new Organization();
        const int count = 100_000;

        Stopwatch stopwatch = Stopwatch.StartNew();

        for (int i = 0; i < count; i++)
        {
            org.Add(new Person(i.ToString(), i));
        }

        stopwatch.Stop();

        System.Console.WriteLine(stopwatch.ElapsedMilliseconds);

    }
}
