using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[60];

    // 컨테이너 초기화
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].item = new Item();
            Slots[i].amount = 0;
        }
    }
}