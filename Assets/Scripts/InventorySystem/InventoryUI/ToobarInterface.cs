using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToobarInterface : UIInventoryPage
{
    private void OnEnable()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].onBeforeUpdated += OnslotUpdate;
        }
    }

    // �ʱ� ������ ������Ʈ
    private void Start()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].onBeforeUpdated.Invoke(inventory.GetSlots[i]);
        }
    }

    // Inventory UI ������Ʈ
    private void OnslotUpdate(InventorySlot _slot)
    {
        if (_slot.item.Id <= -1)
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        }
        else
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.GetItemObject().uiDisplay;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            _slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = !_slot.GetItemObject().stackable ? string.Empty : _slot.amount.ToString("n0");
        }
    }

    // �κ��丮 ���� ����
    public override void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            // ���� ���� �� 
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.gameObject.name = itemPrefab.name + i.ToString();
            uiItem.gameObject.transform.GetChild(2).GetComponentInChildren<TextMeshProUGUI>().text = (i+1).ToString();

            inventory.GetSlots[i].slotDisplay = uiItem;
            slotsOnInterface.Add(uiItem, inventory.GetSlots[i]);

            // �ڵ鷯 ����
            uiItem.OnItemClicked += HandleItemSelection;
            uiItem.OnItemDrag += HandleDrag;
            uiItem.OnItemBeginDrag += HandleBeginDrag;
            uiItem.OnItemEndDrag += HandleEndDrag;
            uiItem.OnItemDroppedOn += HandleSwap;
            uiItem.OnRightMouseBtnClick += HandleShowItemActions;
        }
    }
}
