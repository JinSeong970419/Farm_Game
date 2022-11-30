using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Object", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabase database;

    [SerializeField] public Inventory Container = new Inventory();
    public InventorySlot[] GetSlots => Container.Slots;

    // æ∆¿Ã≈€ √ﬂ∞°
    public bool AddItem(Item _item, int _amount)
    {
        if (EmptySlotCount <= 0) { return false; }

        if (GetEmptySlot() == null)
        {
            Debug.Log("∞°µÊ √°¿Ω");
            return false;
        }

        InventorySlot slot = FindItemOnInventory(_item);
        if (!database.ItemObject[_item.Id].stackable || slot == null)
        {
            GetEmptySlot().UpdateSlot(_item, _amount);
            return true;
        }
        slot.AddAmount(_amount);
        return true;
    }

    public int EmptySlotCount
    {
        get
        {
            int counter = 0;
            for (int i = 0; i < GetSlots.Length; i++)
            {
                counter++;
            }
            return counter;
        }
    }

    // ∫Û slot √£±‚
    public InventorySlot GetEmptySlot()
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1) { return GetSlots[i]; }
        }

        // ¿Œ∫•≈‰∏Æ∞° ∞°µÊ√°¿ª ∞ÊøÏ
        return null;
    }

    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id == _item.Id) { return GetSlots[i]; }
        }
        return null;
    }
}