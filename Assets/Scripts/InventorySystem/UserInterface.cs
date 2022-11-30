using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// �ʼ� ���� ��� ���Ӽ����� �ڵ� �߰�
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
