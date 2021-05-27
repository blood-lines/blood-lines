using System.Text;
using UnityEditor;
using UnityEngine;

public class Person
{
    public Person(Genome genome)
    {
        Genome = genome;
    }

    public int Str => GetStatValue(Stat.Str);
    public int Vit => GetStatValue(Stat.Vit);
    public int Dex => GetStatValue(Stat.Dex);
    public int Agi => GetStatValue(Stat.Agi);
    public int Int => GetStatValue(Stat.Int);
    public int Per => GetStatValue(Stat.Per);

    private int GetStatValue(Stat stat)
    {
        var value = 0;
        foreach (var pair in Genome.ChromosomePairs)
        {
            var c1 = pair.First;
            var c2 = pair.Second;
            for (var i = 0; i < c1.Alleles.Count; i++)
            {
                var a1 = c1.Alleles[i];
                var a2 = c2.Alleles[i];
                if (a1.Trait.Stat != stat)
                {
                    continue;
                }
                if (a1.IsDominant && !a2.IsDominant)
                {
                    value += a1.Trait.Value;
                }
                else if (a2.IsDominant && !a1.IsDominant)
                {
                    value += a2.Trait.Value;
                }
                else
                {
                    value += (a1.Trait.Value + a2.Trait.Value) / 2;
                }
            }
        }

        return value;
    }

    public Genome Genome { get; }

    public Person ProduceOffspring(Person mate)
    {
        var zygote = Genome.CreateZygote(Genome, mate.Genome);
        return new Person(zygote);
    }

    [MenuItem("A/A")]
    public static void A()
    {
        var g1 = GenomeDatabase.Instance.GenerateGenome();
        var p1 = new Person(g1);
        var g2 = GenomeDatabase.Instance.GenerateGenome();
        var p2 = new Person(g2);
        var p3 = p1.ProduceOffspring(p2);

        var sb = new StringBuilder();
        sb.AppendLine($"P1 | STR {p1.Str} | VIT {p1.Vit} | DEX {p1.Dex} | AGI {p1.Agi} | INT {p1.Int} | PER {p1.Per}");
        sb.AppendLine($"P2 | STR {p2.Str} | VIT {p2.Vit} | DEX {p2.Dex} | AGI {p2.Agi} | INT {p2.Int} | PER {p2.Per}");
        sb.AppendLine($"P3 | STR {p3.Str} | VIT {p3.Vit} | DEX {p3.Dex} | AGI {p3.Agi} | INT {p3.Int} | PER {p3.Per}");
        Debug.LogError(sb);
    }

}
