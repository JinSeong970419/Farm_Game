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

    [SerializeField] private UIInventoryDescription itemDescription;

    [SerializeField] private MouseFollower mouseFollower;

    List<UIInventoryItem> listOfUIItems = new List<UIInventoryItem>();

    public Sprite image;
    public int quantity;
    public string title, description;

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
            listOfUIItems.Add(uiItem);
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
        listOfUIItems[0].Select();
    }

    // Drag
    // 1. ����
    private void HandleBeginDrag(UIInventoryItem obj)
    {
        //mouseFollower.Toggle(true);
        //mouseFollower.SetData(image,quantity);

        GameObject temItem = new GameObject();
        var rt = temItem.AddComponent<RectTransform>();
        rt.sizeDelta = new Vector2(100, 100); // 80f80f
        temItem.transform.SetParent(transform.parent.parent);
        temItem.name = "Test";
        var img = temItem.AddComponent<Image>();
        img.sprite = obj.itemImage.sprite;
        MouseData.tempItemBeingDragged = temItem;
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
            //slotsOnInterface[obj].RemoveItem();
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

    //public GameObject CreateTempItem(GameObject obj)
    //{
    //    GameObject temItem = null;
    //    if (slotsOnInterface[obj].item.Id >= 0)
    //    {
    //        temItem = new GameObject();
    //        var rt = temItem.AddComponent<RectTransform>();
    //        rt.sizeDelta = new Vector2(100, 100); // 80f80f
    //        temItem.transform.SetParent(transform.parent.parent);
    //        temItem.name = slotsOnInterface[obj].GetItemObject().name;
    //        var img = temItem.AddComponent<Image>();
    //        img.sprite = slotsOnInterface[obj].GetItemObject().uiDisplay;
    //        img.raycastTarget = false;
    //    }
    //    return temItem;
    //}

    #region �κ��丮 On/Off
    // �κ��丮 �¿��� 
    public void Show()
    {
        gameObject.SetActive(true);
        itemDescription.ResetDescription();

        listOfUIItems[0].SetData(image,quantity);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
    #endregion 
}