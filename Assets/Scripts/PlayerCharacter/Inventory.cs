using System;
using System.Collections.Generic;

public class Inventory
{
    private Dictionary<String, int> inventoryItems;

    public Inventory()
    {
        inventoryItems = new Dictionary<string, int>();
    }
    internal void Add(string reference)
    {
        if (!inventoryItems.ContainsKey(reference)) 
        {
            inventoryItems[reference] = 1;
        }
        else
        {
            inventoryItems[reference]++;
        }
    }
}