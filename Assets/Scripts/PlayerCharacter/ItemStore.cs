using System;
using System.Collections.Generic;

public class ItemStore
{
    private Dictionary<String, int> inventoryItems;

    public ItemStore()
    {
        inventoryItems = new Dictionary<string, int>();
    }

	public bool Has(string itemName) 
	{
		return inventoryItems.ContainsKey(itemName);
	}

	public bool Has(string itemName, int amount, bool exact = false)
	{
		if (!Has(itemName))
		{
			return false;
		}
		else if (exact)
		{
			return inventoryItems[itemName] == amount;
		}
		else
		{
			return inventoryItems[itemName] >= amount;
		}
	}

    public void Add(string itemName, int amount = 1)
    {
		if (amount <= 0) return;

        if (!inventoryItems.ContainsKey(itemName)) 
        {
            inventoryItems[itemName] = amount;
        }
        else
        {
            inventoryItems[itemName] += amount;
        }
    }

	// Removes 1, or a stated amount, of the given item leaving a minimum of zero
	// Returns true if the full amount was dropped
	public bool Drop(string itemName, int amount = 1)
	{
		if (amount <= 0) return false;
		if (!inventoryItems.ContainsKey(itemName)) return false;

		if (inventoryItems[itemName] < amount)
		{
			inventoryItems.Remove(itemName);
			return false; 
		}
		else if (inventoryItems[itemName] == amount)
		{
			inventoryItems.Remove(itemName);
			return true;
		}
		else 
		{
			inventoryItems[itemName] -= amount;
			return true;
		}
	}
}