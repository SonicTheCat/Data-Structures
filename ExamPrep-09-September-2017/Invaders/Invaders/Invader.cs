using System;

public class Invader : IInvader
{
    public Invader(int damage, int distance)
    {
        this.Damage = damage;
        this.Distance = distance;
    }
    
    public int Damage { get; set; }
    public int Distance { get; set; }

    public int CompareTo(IInvader other)
    {
        var cmp = this.Distance.CompareTo(other.Distance);
        if (cmp == 0)
        {
            cmp = other.Damage.CompareTo(this.Damage); 
        }

        return cmp;
    }

    public override string ToString()
    {
        return "Distance: " + this.Distance + " Damage: " + this.Damage;
    }
}
