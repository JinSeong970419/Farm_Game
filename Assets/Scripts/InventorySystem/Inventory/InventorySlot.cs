using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    private ItemType[] AllowedItems = new ItemType[0];

    [NonSerialized] public UIInventoryPage parent;
    [NonSerialized] public UIInventoryItem slotDisplay;

    [NonSerialized] public Action<InventorySlot> onAfterUpdated;
    [NonSerialized] public Action<InventorySlot> onBeforeUpdated;
    [NonSerialized] public Action<Inventory> onsaveUpdated;

    public Item item;
    public int amount;

    // 데이터베이스에서 정보 아이템 가져오기
    public ItemObject GetItemObject() { return item.Id >= 0 ? parent.inventory.database.ItemObject[item.Id] : null; }

    // 아이템 초기화
    public InventorySlot() => UpdateSlot(new Item(), 0);

    public InventorySlot(Item _item, int _amount) => UpdateSlot(_item, _amount);

    // 아이템 삭제
    public void RemoveItem() => UpdateSlot(new Item(), 0);

    // Stackable 속성 아이템 개수 변경
    public void AddAmount(int value) => UpdateSlot(item, amount += value);

    // 아이템슬롯 최신화
    public void UpdateSlot(Item itemValue, int amountValue)
    {
        // 장비창 및 인벤토리창 아이템 전부 삭제 후 불러오기
        item = itemValue;
        amount = amountValue;
        onAfterUpdated?.Invoke(this);
        onBeforeUpdated?.Invoke(this);
        onsaveUpdated?.Invoke(parent?.inventory.Container);
    }

    // 교환이 가능한지 여부 확인
    public bool CanPlaceInSlot(ItemObject itemObject)
    {
        if (AllowedItems.Length <= 0 || itemObject == null || itemObject.data.Id < 0)
        {
            return true;
        }
        // 장착하고자 하는 아이템이 슬롯과 같은 타입인지 확인
        for (int i = 0; i < AllowedItems.Length; i++)
        {
            if (itemObject.type == AllowedItems[i])
            {
                return true;
            }
        }
        return false;
    }
}