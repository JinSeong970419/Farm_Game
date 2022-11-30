using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// 필수 구성 요소 종속성으로 자동 추가
[RequireComponent(typeof(EventTrigger))]
public abstract class UserInterface : MonoBehaviour
{
    public InventoryObject inventory;
    public GameObject inventoryPrefab;

    public Dictionary<GameObject, InventorySlot> slotsOnInterface = new Dictionary<GameObject, InventorySlot>();

    public int X_START;
    public int Y_START;
    public int X_SPACE_BETWEEN_ITEM;
    public int Y_SPACE_BWTWEEN_ITEMS;
    public int NUMBER_OF_COLUMN;

    public abstract void CreateSlots();

    public abstract Vector3 GetPosition(int i);    
}
