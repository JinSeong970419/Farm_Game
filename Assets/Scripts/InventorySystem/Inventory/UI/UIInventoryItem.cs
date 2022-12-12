using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour, IPointerClickHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
{
    [SerializeField] public Image itemImage;       // 아이템 Image
    [SerializeField] private TMP_Text quantityTxt; // 개수 Txt
    [SerializeField] public Image borderImage;    // 선택 슬롯 Hight 효과

    public event Action<UIInventoryItem> OnItemClicked, OnItemDroppedOn, OnItemDrag,
    OnItemBeginDrag, OnItemEndDrag, OnRightMouseBtnClick;

    private void Awake() 
    {
        Deselect();
    }

    public void Deselect() { borderImage.enabled = false; }

    public void Select() { borderImage.enabled = true; }

    public void OnBeginDrag(PointerEventData eventData) { OnItemBeginDrag?.Invoke(this); }
    public void OnDrag(PointerEventData eventData) { OnItemDrag?.Invoke(this); }
    public void OnEndDrag(PointerEventData eventData) { OnItemEndDrag?.Invoke(this); }

    public void OnDrop(PointerEventData eventData) { OnItemDroppedOn?.Invoke(this); }

    public void OnPointerClick(PointerEventData pointerData)
    {
        if (pointerData.button == PointerEventData.InputButton.Right) { OnRightMouseBtnClick?.Invoke(this); }
        else { OnItemClicked?.Invoke(this); }
    }
}
