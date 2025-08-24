using UnityEngine;

public class Bag
{
    public int Capacity { get; private set; }
    public int Count { get; private set; }
    
    public bool IsFull => Count == Capacity;
    
    public int FreeCapacity => Capacity - Count;
    
    public Bag(BagScriptableObject so)
    {
        Capacity = so.Capacity;
    }

    public void AddCookie(int amount)
    {
        if (!IsFull)
            Count += amount;
    }
}
