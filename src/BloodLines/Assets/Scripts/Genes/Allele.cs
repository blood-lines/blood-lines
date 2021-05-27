public class Allele
{
    public Allele(Gene gene, int variant, bool isDominant, Trait trait)
    {
        Gene = gene;
        Variant = variant;
        IsDominant = isDominant;
        Trait = trait;
    }
    
    public Gene Gene { get; }
    public int Variant { get; }
    public bool IsDominant { get; }
    public Trait Trait { get; }
}