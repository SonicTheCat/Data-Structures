public class Program
{
    static void Main(string[] args)
    {
        var invader1 = new Invader(110, 5);
        var invader2 = new Invader(20, 1);
        var invader3 = new Invader(10, 2);
        var invader4 = new Invader(30, 3);
        var invader5 = new Invader(0, 3);

       // var computer = new Computer(100);

        //computer.AddInvader(invader1);
        //computer.AddInvader(invader2);
        //computer.AddInvader(invader3);
        //computer.AddInvader(invader4);
        //computer.AddInvader(invader5);

        //computer.Skip(3); 
        //computer.Skip(3); 

        Computer computer = new Computer(100);
        //var one = 1;
        for (int i = 0; i < 10; i++)
        {
           // System.Console.WriteLine(one++);
            var invader = new Invader(10, 10 + i);
            computer.AddInvader(invader);
        }
        System.Console.WriteLine();
    }
}
