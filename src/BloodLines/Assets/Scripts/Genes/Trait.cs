public class Trait
{
    public Trait(Stat stat, int value)
    {
        Stat = stat;
        Value = value;
    }
    
    public Stat Stat { get; }
    
    public int Value { get; }
}