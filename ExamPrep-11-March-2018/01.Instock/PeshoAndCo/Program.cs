
class Program
{
    static void Main(string[] args)
    {
        Instock products = new Instock();

        var pr1 = new Product("salam", 10, 2);
        var pr2 = new Product("masla", 10, 1);
        var pr3 = new Product("bob", 10, 3);

        products.Add(pr1);
        products.Add(pr2);
        products.Add(pr3);

        var res = products.FindAllByPrice(10);

        System.Console.WriteLine(   );
    }
}

