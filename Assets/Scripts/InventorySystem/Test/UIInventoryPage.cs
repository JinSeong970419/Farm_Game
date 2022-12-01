using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class UIInventoryPage : MonoBehaviour
{
    [SerializeField] protected UIInventoryItem itemPrefab; // ���� ������
    [SerializeField] protected RectTransform contentPanel; // ���� ���� ������Ʈ

    [SerializeField] public InventoryObject inventory; // �κ��丮 ������

    public Dictionary<UIInventoryItem, InventorySlot> slotsOnInterface = new Dictionary<UIInventoryItem, InventorySlot>();

    // �κ��丮 ���� ����
    public abstract void InitializeInventoryUI(int inventorySize);

    // ��Ŭ��
    protected void HandleItemSelection(UIInventoryItem obj)
    {
        // ������ ���� â
        if(slotsOnInterface[obj].item.Id > -1)
        {
            Debug.Log("������~");
            //itemDescription.SetDescription(slotsOnInterface[obj].GetItemObject());
        }
    }

    // ��Ŭ��
    protected void HandleShowItemActions(UIInventoryItem obj)
    {
    }

    // Drag
    // 1. ����
    protected void HandleBeginDrag(UIInventoryItem obj)
    {
        MouseData.slotHoveredOver = slotsOnInterface[obj];   // ���� ���� ����
        MouseData.tempItemBeingDragged = CreateTempItem(obj); // drag �̹��� ����
    }
    // 2. ����
    protected void HandleDrag(UIInventoryItem obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
        }
    }

    // 3. ����
    protected void HandleEndDrag(UIInventoryItem obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
        // ������ ����
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            slotsOnInterface[obj].RemoveItem();
        }
        // ������ ��ġ ����
        else if (MouseData.interfaceMouseIsOver != null)
        {
            inventory.SwapItems(MouseData.slotHoveredOver, MouseData.interfaceMouseIsOver);
            MouseData.interfaceMouseIsOver = null;
        }
    }

    protected void HandleSwap(UIInventoryItem obj)
    {
        MouseData.interfaceMouseIsOver = slotsOnInterface[obj]; // ���� ���� ����
    }

    // ������ �巹�� �� ������ ����
    protected GameObject CreateTempItem(UIInventoryItem obj)
    {
        GameObject temItem = null;
        if (slotsOnInterface[obj].item.Id >= 0)
        {
            temItem = new GameObject();
            var rt = temItem.AddComponent<RectTransform>();
            rt.sizeDelta = new Vector2(100, 100); // 80f80f
            temItem.transform.SetParent(transform.parent.parent);
            temItem.name = slotsOnInterface[obj].GetItemObject().name;
            var img = temItem.AddComponent<Image>();
            img.sprite = slotsOnInterface[obj].GetItemObject().uiDisplay;
            img.raycastTarget = false;
        }
        return temItem;
    }
}