using System;

public class Gene
{
    public string Id { get; set; } = Guid.NewGuid().ToString().Substring(0, 4);
}