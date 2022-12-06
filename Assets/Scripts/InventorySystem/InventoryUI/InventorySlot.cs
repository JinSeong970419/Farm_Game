using System;
using UnityEngine;

[Serializable]
public class InventorySlot
{
    public ItemType[] AllowedItems = new ItemType[0];

    [NonSerialized] public UIInventoryPage parent;
    [NonSerialized] public UIInventoryItem slotDisplay;
    //[NonSerialized] public GameObject slotDisplay;

    [NonSerialized] public Action<InventorySlot> onAfterUpdated;
    [NonSerialized] public Action<InventorySlot> onBeforeUpdated;

    public Item item;
    public int amount;

    // �����ͺ��̽����� ���� ������ ��������
    public ItemObject GetItemObject() { return item.Id >= 0 ? parent.inventory.database.ItemObject[item.Id] : null; }

    // ������ �ʱ�ȭ
    public InventorySlot() => UpdateSlot(new Item(), 0);

    public InventorySlot(Item _item, int _amount) => UpdateSlot(_item, _amount);

    // ������ ����
    public void RemoveItem() => UpdateSlot(new Item(), 0);

    // Stackable �Ӽ� ������ ���� ����
    public void AddAmount(int value) => UpdateSlot(item, amount += value);

    // �����۽��� �ֽ�ȭ
    public void UpdateSlot(Item itemValue, int amountValue)
    {
        // ���â �� �κ��丮â ������ ���� ���� �� �ҷ�����
        item = itemValue;
        amount = amountValue;
        onAfterUpdated?.Invoke(this);
        onBeforeUpdated?.Invoke(this);
    }

    // ��ȯ�� �������� ���� Ȯ��
    public bool CanPlaceInSlot(ItemObject itemObject)
    {
        if (AllowedItems.Length <= 0 || itemObject == null || itemObject.data.Id < 0)
        {
            return true;
        }
        // �����ϰ��� �ϴ� �������� ���԰� ���� Ÿ������ Ȯ��
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