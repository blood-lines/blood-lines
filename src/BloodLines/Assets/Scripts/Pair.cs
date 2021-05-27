using System;

public struct Pair<T>
{
    public Pair(T first, T second)
    {
        First = first;
        Second = second;
    }
    
    public T First { get; }
    public T Second { get; }

    public Pair<T2> Select<T2>(Func<T, T2> selector)
    {
        return new Pair<T2>(selector(First), selector(Second));
    }
}