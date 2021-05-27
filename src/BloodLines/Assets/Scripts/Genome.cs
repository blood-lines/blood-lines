using System;
using System.Collections.Generic;
using System.Linq;

public class Genome
{
    private static readonly Random Random = new Random();

    public Genome(List<Pair<Chromosome>> chromosomePairs)
    {
        ChromosomePairs = chromosomePairs;
    }
    
    public List<Pair<Chromosome>> ChromosomePairs { get; }

    public Pair<Allele> GetAlleles(Gene gene)
    {
        foreach (var chromosomePair in ChromosomePairs)
        {
            var alleles = chromosomePair.Select(c => c.Alleles.Find(a => a.Gene == gene));
            if (alleles.First != null)
            {
                return alleles;
            }
        }

        return new Pair<Allele>();
    }

    public List<Chromosome> Meiosis()
    {
        const double minRecombinationRate = 0;
        const double maxRecombinationRate = 0.5;

        var recombinationRate = minRecombinationRate + (maxRecombinationRate - minRecombinationRate) * Random.NextDouble();
        var chromosomes = new List<Chromosome>();
        foreach (var pair in ChromosomePairs)
        {
            var baseChromosome = Random.NextDouble() > 0.5 ? pair.First : pair.Second;
            var recombinationChromosome = baseChromosome == pair.First ? pair.Second : pair.First;
            var newAlleles = baseChromosome.Alleles.Zip(
                recombinationChromosome.Alleles,
                (a1, a2) => Random.NextDouble() > recombinationRate ? a1 : a2
            );

            chromosomes.Add(new Chromosome(newAlleles.ToList()));
        }

        return chromosomes;
    }

    public static Genome ProduceOffspring(Genome first, Genome second)
    {
        var firstChromosomes = first.Meiosis();
        var secondChromosomes = second.Meiosis();
        var chromosomePairs = firstChromosomes.Zip(
            secondChromosomes,
            (c1, c2) => new Pair<Chromosome>(c1, c2)
        );

        return new Genome(chromosomePairs.ToList());
    }
}