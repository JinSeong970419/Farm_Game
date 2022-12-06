using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryDescription : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text title;
    [SerializeField] private TMP_Text description;

    public void Awake() { ResetDescription(); }

    // 아이템 설명창 초기화
    public void ResetDescription()
    {
        itemImage.gameObject.SetActive(false);
        title.text = "";
        description.text = "";
    }

    // 아이템 설명창 추가
    public void SetDescription(ItemObject obj)
    {
        itemImage.gameObject.SetActive(true);
        itemImage.sprite = obj.uiDisplay;
        title.text = obj.ObjName;
        description.text = obj.description;
    }
}
