public class Allele
{
    public Allele(Gene gene, int variant, bool isDominant)
    {
        Gene = gene;
        Variant = variant;
        IsDominant = isDominant;
    }
    
    public Gene Gene { get; }
    public int Variant { get; }
    public bool IsDominant { get; }
}