using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

// ToolBar 슬롯
public class Toolbar_UI : MonoBehaviour
{
    private UIInventoryItem[] toolbarSlots;
    private List<UIInventoryItem> Slots = new List<UIInventoryItem>();

    public static UnityAction<int> checAlphaNumbericKeys; // 슬롯 선택

    private int currentNum;
    private void Awake()
    {
        checAlphaNumbericKeys = (int newNumber) => SelectSlot(newNumber);
    }

    private void Start()
    {
        currentNum = 0;
        toolbarSlots = GetComponentsInChildren<UIInventoryItem>();
        foreach (var toolbarSlot in toolbarSlots)
        {
            Slots.Add(toolbarSlot);
        }
        SelectSlot();
    }

    // 선택된 슬롯창 표시
    public void SelectSlot(int newNumber = 1)
    {
        if (!GameManager.instance._SpliterUI)
        {
            Slots[currentNum].borderImage.enabled = false;
            Slots[newNumber - 1].borderImage.enabled = true;
            currentNum = newNumber - 1;
            MouseData.selectBar = Slots[currentNum];
        }
    }
}