using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public Dictionary<string, Inventorys> inventoryByName = new Dictionary<string, Inventorys>();

    [Header("Backpack")]
    public Inventorys backpack;
    public int backpackSlotCount;

    [Header("Toolbar")]
    public Inventorys toolbar;
    public int toolbarSlotCount;

    private void Awake()
    {
        backpack = new Inventorys(backpackSlotCount);
        toolbar = new Inventorys(toolbarSlotCount);

        inventoryByName.Add("Backpack", backpack);
        inventoryByName.Add("Toolbar", toolbar);
    }

    public void Add(string invetoryName, Item item)
    {
        if (inventoryByName.ContainsKey(invetoryName))
        {
            inventoryByName[invetoryName].Add(item);
        }
    }

    // 인벤토리 이름으로 값 가져오기
    public Inventorys GetInventoryByName(string inventoryName)
    {
        if (inventoryByName.ContainsKey(inventoryName))
        {
            return inventoryByName[inventoryName];
        }

        return null;
    }
}
