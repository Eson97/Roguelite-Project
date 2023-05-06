using CustomExceptions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem
{
    public InventoryItemSO Data { get; set; }
    public int Quantity { get; set; }

    public void Add(int amount) => Quantity += amount;
    public void Remove(int amount)
    {
        if (Quantity == 0 || amount > Quantity)
            throw new NotEnoughItemsException();

        Quantity -= amount;
    }
}
