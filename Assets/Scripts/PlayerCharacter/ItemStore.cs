using System;
using Unity;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine.AI;

public class ItemStore
{
    private Dictionary<String, int> items;

    public ItemStore()
    {
        items = new Dictionary<string, int>();
    }

	public bool Has(string itemName) 
	{
		return items.ContainsKey(itemName);
	}

	public bool Has(string itemName, int amount, bool exact = false)
	{
		if (!Has(itemName))
		{
			return false;
		}
		else if (exact)
		{
			return items[itemName] == amount;
		}
		else
		{
			return items[itemName] >= amount;
		}
	}

    public void Add(string itemName, int amount = 1)
    {
		if (amount <= 0) return;

        if (!items.ContainsKey(itemName)) 
        {
            items[itemName] = amount;
        }
        else
        {
            items[itemName] += amount;
        }
    }

	// Removes 1, or a stated amount, of the given item leaving a minimum of zero
	// Returns true if the full amount was dropped
	public bool Drop(string itemName, int amount = 1)
	{
		if (amount <= 0) return false;
		if (!items.ContainsKey(itemName)) return false;

		if (items[itemName] < amount)
		{
			items.Remove(itemName);
			return false; 
		}
		else if (items[itemName] == amount)
		{
			items.Remove(itemName);
			return true;
		}
		else 
		{
			items[itemName] -= amount;
			return true;
		}
	}
}