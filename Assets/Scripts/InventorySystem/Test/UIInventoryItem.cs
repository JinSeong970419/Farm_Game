using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] public Image itemImage;
    [SerializeField] private TMP_Text quantityTxt;

    [SerializeField] private Image borderImage;

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemDrag, OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private void Awake()
    {
        ResetData();
        Deselect();
    }

    public void ResetData()
    {
        this.itemImage.gameObject.SetActive(false);
    }

    public void Deselect()
    {
        borderImage.enabled = false;
    }

    // 인벤토리 창 초기화
    public void SetData(Sprite sprite, int quantity)
    {
        this.itemImage.gameObject.SetActive(true);
        this.itemImage.sprite = sprite;
        this.quantityTxt.text = quantity + "";
    }

    public void Select()
    {
        borderImage.enabled = true;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (empty)
        //    return;
        OnItemBeginDrag?.Invoke(this);
    }
    public void OnDrag(PointerEventData eventData)
    {
        OnItemDrag?.Invoke(this);
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        OnItemEndDrag?.Invoke(this);
    }

    public void OnDrop(PointerEventData eventData)
    {
        OnItemDroppedOn?.Invoke(this);
    }


    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right)
        {
            OnRightMouseBtnClick?.Invoke(this);
        }
        else
        {
            OnItemClicked?.Invoke(this);
        }
    }
}
