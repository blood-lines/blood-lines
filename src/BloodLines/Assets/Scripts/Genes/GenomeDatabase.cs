using System;
using System.Collections.Generic;
using System.Linq;

public class GenomeDatabase
{
    private GeneDatabase _geneDatabase = new GeneDatabase();
    private List<ChromosomeDatabase> _chromosomes = new List<ChromosomeDatabase>();

    public GenomeDatabase()
    {
        AddChromosome(50);
        AddChromosome(40);
        AddChromosome(30);
        AddChromosome(20);
        AddChromosome(10);
    }

    private void AddChromosome(int geneCount)
    {
        var chromosomeDatabase = new ChromosomeDatabase(_geneDatabase);
        chromosomeDatabase.Populate(geneCount);
        _chromosomes.Add(chromosomeDatabase);
    }

    public Genome GenerateGenome()
    {
        var chromosomes1 = _chromosomes.Select(c => c.GenerateChromosome());
        var chromosomes2 = _chromosomes.Select(c => c.GenerateChromosome());
        var chromosomePairs = chromosomes1.Zip(
            chromosomes2,
            (c1, c2) => new Pair<Chromosome>(c1, c2)
        );
        return new Genome(chromosomePairs.ToList());
    }

    public static GenomeDatabase Instance { get; }  = new GenomeDatabase();
}

public class ChromosomeDatabase
{
    private static readonly Random Random = new Random();

    private readonly GeneDatabase _geneDatabase;

    public ChromosomeDatabase(GeneDatabase geneDatabase)
    {
        _geneDatabase = geneDatabase;
    }

    public List<Gene> Genes { get; } = new List<Gene>();

    public void Populate(int geneCount)
    {
        for (int i = 0; i < geneCount; i++)
        {
            Genes.Add(_geneDatabase.CreateNewGene());
        }
    }

    public Chromosome GenerateChromosome()
    {
        var alleles = Genes.Select(_geneDatabase.GetRandomAllele).ToList();
        return new Chromosome(alleles);
    }
}

public class GeneDatabase
{
    private static readonly Random Random = new Random();
    
    public Dictionary<Gene, List<Allele>> GeneVariations { get; } = new Dictionary<Gene, List<Allele>>();

    public Gene CreateNewGene()
    {
        var gene = new Gene();
        var alleleCount = Random.Next(1, 6);
        var stat = (Stat) Random.Next(0, 7);
        var alleles = new List<Allele>();
        for (int i = 0; i < alleleCount; i++)
        {
            var value = Random.Next(-5, 6);
            var trait = new Trait(stat, value);
            var isDominant = Random.NextDouble() > 0.5;
            var allele = new Allele(gene, i, isDominant, trait);
            alleles.Add(allele);
        }
        
        GeneVariations.Add(gene, alleles);
        return gene;
    }

    public Allele GetRandomAllele(Gene gene)
    {
        var variations = GeneVariations[gene];
        return variations[Random.Next(variations.Count)];
    }
}