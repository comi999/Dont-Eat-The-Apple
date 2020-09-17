using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
    public List<ItemStack> inventory = new List<ItemStack>();

    public string name;
    public int itemCount;
    public int weight;
    public int weightLimit;

    public Inventory(string name, int weightLimit)
    {
        this.name = name;
        itemCount = 0;
        weight = 0;
        this.weightLimit = weightLimit;
    }

    public bool Contains(int id)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == id)
                return true;
        }

        return false;
    }

    public bool CanFit(ItemStack itemStack)
    {
        return itemStack.GetWeight() + weight <= weightLimit;
    }

    public bool Insert(ItemStack itemStack)
    {
        if (!CanFit(itemStack))
            return false;

        itemCount += itemStack.count;
        weight += itemStack.GetWeight();

        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == itemStack.id)
            {
                inventory[i].count += itemStack.count;
                
                return true;
            }
        }

        inventory.Add(itemStack);

        return true;
    }

    public bool Remove(ItemStack itemStack)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].id == itemStack.id)
            {
                int amount = inventory[i].count;

                inventory[i].count -= itemStack.count;

                if (inventory[i].count <= 0)
                {
                    itemCount -= amount;
                    weight -= amount * ItemDatabase.GetItem(itemStack.id).weight;
                    inventory.RemoveAt(i);
                }
                else
                {
                    itemCount -= itemStack.count;
                    weight -= itemStack.GetWeight();
                }

                return true;
            }
        }

        return false;
    }

    public bool TransferTo(ItemStack itemStack, ref Inventory inventory)
    {
        if (!inventory.CanFit(itemStack))
            return false;

        int amount = 0;

        for (int i = 0; i < this.inventory.Count; i++)
        {
            if (this.inventory[i].id == itemStack.id)
            {
                if (this.inventory[i].count > itemStack.count)
                {
                    this.inventory[i].count -= itemStack.count;
                    amount = itemStack.count;
                }
                else
                {
                    amount = this.inventory[i].count;
                    this.inventory.RemoveAt(i);
                }

                break;
            }
        }

        if (amount == 0)
            return false;

        itemCount -= amount;
        weight -= amount * ItemDatabase.GetItem(itemStack.id).weight;

        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            if (inventory.inventory[i].id == itemStack.id)
            {
                inventory.inventory[i].count += amount;
                inventory.weight += amount * ItemDatabase.GetItem(itemStack.id).weight;
                return true;
            }
        }

        inventory.inventory.Add(itemStack);
        
        inventory.itemCount += amount;
        inventory.weight += amount * ItemDatabase.GetItem(itemStack.id).weight;

        return true;
    }

    public void Clear()
    {
        inventory.Clear();
        itemCount = 0;
        weight = 0;
    }
}
