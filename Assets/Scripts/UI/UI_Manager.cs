using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UI_Manager : MonoBehaviour
{
    public Dictionary<string, Inventory_UI> inventoryUIByName = new Dictionary<string, Inventory_UI>();

    public GameObject inventoryPanel;

    public List<Inventory_UI> inventoryUIs;

    public static Slot_UI draggedSlot;
    public static Image draggedIcon;
    public static bool dragSingle;

    public static UnityAction InventoryOnOff; // 인벤토리 온오프 기능
    public static UnityAction SingleOnOff; // 인벤토리 온오프 기능

    private void Awake()
    {
        Initialized();
        InventoryOnOff = () => { ToggleInventoryUI(); };
        SingleOnOff = () => { SingleKey(); };
    }

    private void Initialized()
    {
        foreach (Inventory_UI ui in inventoryUIs)
        {
            inventoryUIByName.Add(ui.inventoryName, ui);
        }
        inventoryPanel.SetActive(false);
    }

    // Inventory Ui On/Off
    public void ToggleInventoryUI()
    {
        if (inventoryPanel != null)
        {
            if (!inventoryPanel.activeSelf)
            {
                inventoryPanel.SetActive(true);
                RefreshInventoryUI("Backpack");
            }
            else
            {
                inventoryPanel.SetActive(false);
            }
        }
    }

    public void SingleKey() { dragSingle = !dragSingle; }

    public void RefreshInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            inventoryUIByName[inventoryName].Refresh();
        }
    }

    public void RefreshAll()
    {
        foreach (KeyValuePair<string, Inventory_UI> keyValuePair in inventoryUIByName)
        {
            keyValuePair.Value.Refresh();
        }
    }

    public Inventory_UI GetInventoryUI(string inventoryName)
    {
        if (inventoryUIByName.ContainsKey(inventoryName))
        {
            return inventoryUIByName[inventoryName];
        }

        Debug.LogWarning("인벤토리 UI가 존제하지 않음" + inventoryName);
        return null;
    }

    
}
