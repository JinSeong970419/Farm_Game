using UnityEngine;

[CreateAssetMenu(fileName = "Inventory Object", menuName = "Inventory System/Inventory")]
public class InventoryObject : ScriptableObject
{
    public ItemDatabase database;
    [SerializeField] private string path;

    [SerializeField] public Inventory Container = new Inventory();
    public InventorySlot[] GetSlots => Container.Slots;

    // 아이템 추가
    public bool AddItem(Item _item, int _amount)
    {
        if (EmptySlotCount <= 0) { return false; }

        if (GetEmptySlot() == null)
        {
#if UNITY_EDITOR
            Debug.Log("가득 찼음");
#endif
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

    // 슬롯이 생성되지 않은 경우 체크
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

    // 빈 slot 찾기
    public InventorySlot GetEmptySlot()
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id <= -1) { return GetSlots[i]; }
        }

        // 인벤토리가 가득찼을 경우
        return null;
    }

    // 인벤토리 찾기
    public InventorySlot FindItemOnInventory(Item _item)
    {
        for (int i = 0; i < GetSlots.Length; i++)
        {
            if (GetSlots[i].item.Id == _item.Id) { return GetSlots[i]; }
        }
        return null;
    }

    // 아이템 위치 변경
    public void SwapItems(InventorySlot item1, InventorySlot item2)
    {
        // 바꾸고자 하는 아이템이 겹칠 수 있고 같다면
        if(item1.item.Id == item2.item.Id && item1.GetItemObject().stackable)
        {
            item2.UpdateSlot(item1.item, item1.amount + item2.amount);
            item1.RemoveItem();
        }

        // 바꾸고자 하는 아이템 정보가 위치를 변경할 수 있는지 확인 후 변경
        else if (item2.CanPlaceInSlot(item1.GetItemObject()) && item1.CanPlaceInSlot(item2.GetItemObject()))
        {
            InventorySlot temp = new InventorySlot(item2.item, item2.amount);
            item2.UpdateSlot(item1.item, item1.amount);
            item1.UpdateSlot(temp.item, temp.amount);
        }
    }

    // 인벤토리 슬롯
    [ContextMenu("Save")] public void Save() { Container.Save(path); } // 인벤토리 저장
    [ContextMenu("Load")] public void Load() { Container.Load(path); } // 인벤토리 로드
    [ContextMenu("Clear")] public void Clear() { Container.Clear(); }  // 인벤 비우기
}