public class Item
{
    public Item(int x1, int y1, string name)
    {
        this.X1 = x1;
        this.Y1 = y1;
        this.Name = name;
    }

    public int X1 { get; set; }
    public int Y1 { get; set; }
    public int X2 { get { return this.X1 + 10; } set { } }
    public int Y2 { get { return this.Y1 + 10; } set { } }
    public string Name { get; set; }

    public bool Intersect(Item that)
    {
        return
            this.X1 <= that.X2
            && this.X2 >= that.X1 
            && this.Y1 <= that.Y2
            && this.Y2 >= that.Y1;
    }
}