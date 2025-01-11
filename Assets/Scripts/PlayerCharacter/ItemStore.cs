using System;
using System.Collections.Generic;
using UnityEngine;

public class ItemStore
{
    protected Dictionary<String, int> Items = new();

	// Does the item store contain this item
	public bool Has(string itemName) 
	{
		return Items.ContainsKey(itemName);
	}

	// Does the item store contain at least, or exactly, 'amount' items
	public bool Has(string itemName, int amount, bool exact = false)
	{
		if (!Has(itemName))
		{
			return false;
		}
		else if (exact)
		{
			return Items[itemName] == amount;
		}
		else
		{
			return Items[itemName] >= amount;
		}
	}

	// Adds item(s) to store
    public void Add(string itemName, int amount = 1)
    {
		if (amount <= 0) return;

        if (!Items.ContainsKey(itemName)) 
        {
            Items[itemName] = amount;
        }
        else
        {
            Items[itemName] += amount;
        }
    }

	// Removes 1, or a stated amount, of the given item leaving a minimum of zero
	// Returns true if the full amount was dropped
	public bool Drop(string itemName, int amount = 1)
	{
		if (amount <= 0) return false;
		if (!Items.ContainsKey(itemName)) return false;

		if (Items[itemName] < amount)
		{
			Items.Remove(itemName);
			return false; 
		}
		else if (Items[itemName] == amount)
		{
			Items.Remove(itemName);
			return true;
		}
		else 
		{
			Items[itemName] -= amount;
			return true;
		}
	}
}