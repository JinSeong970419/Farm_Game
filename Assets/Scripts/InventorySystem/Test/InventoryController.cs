using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryController : MonoBehaviour
{
    [SerializeField] private UIInventoryPage inventoryUI;
    public InputReader _inputReader;

    public int inventorySize = 10;

    private void OnEnable()
    {
        _inputReader.Inventory += OnInventory;
    }

    private void OnDisable()
    {
        _inputReader.Inventory -= OnInventory;
    }

    private void Start()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
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
