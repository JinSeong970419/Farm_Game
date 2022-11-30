using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIInventoryPage : MonoBehaviour
{
    // ���� ������
    [SerializeField] private UIInventoryItem itemPrefab;

    [SerializeField] private RectTransform contentPanel;
    public InventoryObject inventory;

    [SerializeField] private UIInventoryDescription itemDescription;

    //List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();
    public Dictionary<UIInventoryItem, InventorySlot> slotsOnInterface = new Dictionary<UIInventoryItem, InventorySlot>();

    public Sprite image;
    public int quantity;
    public string title, description;

    private void OnEnable()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            inventory.GetSlots[i].parent = this;
            inventory.GetSlots[i].onAfterUpdated += OnslotUpdate;
        }
    }

    private void OnslotUpdate(InventorySlot _slot)
    {
        Debug.Log("������Ʈ �̺�Ʈ 2");
        if (_slot.item.Id <= -1)
        {
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
            //_slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = string.Empty;

        }
        else
        {

            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().gameObject.SetActive(true);
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().sprite = _slot.GetItemObject().uiDisplay;
            _slot.slotDisplay.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
            //_slot.slotDisplay.GetComponentInChildren<TextMeshProUGUI>().text = _slot.amount == 1 ? string.Empty : _slot.amount.ToString("n0");
        }
    }

    private void Awake()
    {
        Hide();
        itemDescription.ResetDescription();
    }

    // �κ��丮 ���� ����
    public void InitializeInventoryUI(int inventorysize)
    {
        for (int i = 0; i < inventorysize; i++)
        {
            UIInventoryItem uiItem = Instantiate(itemPrefab, Vector3.zero, Quaternion.identity);
            uiItem.transform.SetParent(contentPanel);
            uiItem.gameObject.name = itemPrefab.name + i.ToString();

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

    // ��Ŭ��
    private void HandleItemSelection(UIInventoryItem obj)
    {
        // ������ ���� â
        itemDescription.SetDescription(image, title, description);
        //listOfUIItems[0].Select();
    }

    // Drag
    // 1. ����
    private void HandleBeginDrag(UIInventoryItem obj)
    {
        MouseData.tempItemBeingDragged = CreateTempItem(obj);
    }
    // 2. ����
    private void HandleDrag(UIInventoryItem obj)
    {
        if (MouseData.tempItemBeingDragged != null)
        {
            MouseData.tempItemBeingDragged.GetComponent<RectTransform>().position = Mouse.current.position.ReadValue();
        }
    }

    // 3. ����
    private void HandleEndDrag(UIInventoryItem obj)
    {
        //mouseFollower.Toggle(false);

        Destroy(MouseData.tempItemBeingDragged);
        // ������ ����
        if (MouseData.interfaceMouseIsOver == null)
        {
            slotsOnInterface[obj].RemoveItem();
            return;
        }
        // ������ ��ġ ����
    }

    private void HandleSwap(UIInventoryItem obj)
    {
    }


    private void HandleShowItemActions(UIInventoryItem obj)
    {
    }

    public GameObject CreateTempItem(UIInventoryItem obj)
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

    #region �κ��丮 On/Off
    // �κ��丮 �¿��� 
    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion 
}