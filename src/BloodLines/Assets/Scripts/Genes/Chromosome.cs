using System.Collections.Generic;

public class Chromosome
{
    public Chromosome(List<Allele> alleles)
    {
        Alleles = alleles;
    }
    
    public List<Allele> Alleles { get; }
}