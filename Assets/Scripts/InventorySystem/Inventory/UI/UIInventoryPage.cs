using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public abstract class UIInventoryPage : MonoBehaviour
{
    [SerializeField] protected UIInventoryItem itemPrefab; // 슬롯 프리팹
    [SerializeField] protected RectTransform contentPanel; // 슬롯 생성 오브젝트

    [SerializeField] public InventoryObject inventory; // 인벤토리 데이터
    [SerializeField] public SaveSystem saveSystem;     // Save 데이터

    public Dictionary<UIInventoryItem, InventorySlot> slotsOnInterface = new Dictionary<UIInventoryItem, InventorySlot>();

    // 인벤토리 슬롯 생성
    public abstract void InitializeInventoryUI(int inventorySize);

    // 좌클릭
    protected void HandleItemSelection(UIInventoryItem obj)
    {
        // 아이템 설명 창
        if(slotsOnInterface[obj].item.Id > -1)
        {
            Debug.Log("설명중~");
            //itemDescription.SetDescription(slotsOnInterface[obj].GetItemObject());
        }
    }

    // Drag
    // 1. 시작
    protected void HandleBeginDrag(UIInventoryItem obj)
    {
        MouseData.slotHoveredOver = slotsOnInterface[obj];   // 시작 슬롯 정보
        MouseData.tempItemBeingDragged = CreateTempItem(obj); // drag 이미지 생성
    }
    // 2. 진행
    protected void HandleDrag(UIInventoryItem obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
        }
    }

    // 3. 종료
    protected void HandleEndDrag(UIInventoryItem obj)
    {
        Destroy(MouseData.tempItemBeingDragged);
        // 아이템 삭제
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            slotsOnInterface[obj].RemoveItem();
        }
        // 아이템 위치 변경
        else if (MouseData.interfaceMouseIsOver != null)
        {
            inventory.SwapItems(MouseData.slotHoveredOver, MouseData.interfaceMouseIsOver);
            MouseData.interfaceMouseIsOver = null;
        }
    }

    protected void HandleSwap(UIInventoryItem obj)
    {
        MouseData.interfaceMouseIsOver = slotsOnInterface[obj]; // 도착 슬롯 정보
    }

    // 아이템 드레그 중 아이콘 생성
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

    private void OnApplicationQuit()
    {
        saveSystem.SaveTest(1, saveSystem.nowSlot);
        inventory.Clear();
    }
}