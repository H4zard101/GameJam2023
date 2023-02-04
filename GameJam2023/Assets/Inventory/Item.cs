using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        Seeds,
        Water
    }

    public ItemType itemType;

    public int amount;
    public int SeedAmount = 10;
    public int WaterAmount = 10;
}
