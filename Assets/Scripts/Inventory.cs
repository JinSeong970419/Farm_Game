using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Inventory
{
    public InventorySlot[] Slots = new InventorySlot[27];

    // 컨테이너 초기화
    public void Clear()
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            //Slots[i].item = new Item();
            Slots[i].amount = 0;
        }
    }

    //public bool ContainsItem(ItemObject itemObject) { return Array.Find(Slots, i => i.item.Id == itemObject.data.Id) != null; }

    //public bool ContainsItem(int id) { return Slots.FirstOrDefault(i => i.item.Id == id) != null; }
}

//[System.Serializable]
public class Inventorys
{
    public class Slot
    {
        public string itemName;
        public int count;
        public int maxAllowed;

        public Sprite icon;

        public Slot()
        {
            itemName = "";
            count = 0;
            maxAllowed = 99;
        }

        public bool IsEmpty
        {
            get
            {
                if (itemName == "" && count == 0)
                {
                    return true;
                }
                return false;
            }
        }

        public bool CanAddItem(string itemName)
        {
            if (this.itemName == itemName && count < maxAllowed)
            {
                return true;
            }
            return false;
        }

        public void AddItem(Item item)
        {
            //this.itemName = item.data.itemName;
            //this.icon = item.data.icon;
            count++;
        }

        public void AddItem(string itemName, Sprite icon, int maxAllowed)
        {
            this.itemName = itemName;
            this.icon = icon;
            count++;
            this.maxAllowed = maxAllowed;
        }

        public void RemoveItem()
        {
            if (count > 0)
            {
                count--;

                if (count == 0)
                {
                    icon = null;
                    itemName = "";
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventorys(int numSlots)
    {
        for (int i = 0; i < numSlots; i++)
        {
            Slot slot = new Slot();
            slots.Add(slot);
        }
    }

    public void Add(Item item)
    {
        //foreach (Slot slot in slots)
        //{
        //    if (slot.itemName == item.data.itemName && slot.CanAddItem(item.data.itemName))
        //    {
        //        slot.AddItem(item);
        //        return;
        //    }
        //}

        //foreach (Slot slot in slots)
        //{
        //    if (slot.itemName == "")
        //    {
        //        slot.AddItem(item);
        //        return;
        //    }
        //}
    }

    public void Remove(int index)
    {
        slots[index].RemoveItem();
    }

    public void Remove(int index, int numToRemove)
    {
        if (slots[index].count >= numToRemove)
        {
            for (int i = 0; i < numToRemove; i++)
            {
                Remove(index);
            }
        }
    }

    public void MoveSlot(int fromIndex, int toIndex, Inventorys toInventory, int numToMove = 1)
    {
        Slot fromSlot = slots[fromIndex];
        Slot toSlot = toInventory.slots[toIndex];

        if (toSlot.IsEmpty || toSlot.CanAddItem(fromSlot.itemName))
        {
            for (int i = 0; i < numToMove; i++)
            {
                Debug.Log("실행 : " + i);
                toSlot.AddItem(fromSlot.itemName, fromSlot.icon, fromSlot.maxAllowed);
                fromSlot.RemoveItem();
            }
        }
    }

}