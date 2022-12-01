using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// Movement ÇÕÄ¡±â?
public class InventoryController : MonoBehaviour
{
    //[SerializeField] private UIInventoryPage inventoryUI;
    [SerializeField] private InManager inventoryUI;
    [SerializeField] private ToobarInterface ToolbarUI;
    public InputReader _inputReader;

    public int inventorySize;
    public int toolbarSize;

    private void OnEnable()
    {
        _inputReader.Inventory += OnInventory;
    }

    private void OnDisable()
    {
        _inputReader.Inventory -= OnInventory;
    }

    private void Awake()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
        ToolbarUI.InitializeInventoryUI(toolbarSize);
    }

    // Inventory Ui On/Off
    private void OnInventory()
    {
        if (inventoryUI.isActiveAndEnabled == false)
        {
            inventoryUI.Show();
        }
        else
        {
            inventoryUI.Hide();
        }
    }
}
