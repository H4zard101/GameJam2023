using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        Array values = Enum.GetValues(typeof(Item.ItemType));
        foreach (Item.ItemType type in values)
        {
            AddItem(new Item{ itemType = type, amount = 0});
        }
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void UpdateItemTypeAmount(Item.ItemType type, int amount)
    {
        foreach (Item item in itemList)
        {
            if (item.itemType == type)
            {
                item.amount = amount;
                break;
            }
        }
    }
}
