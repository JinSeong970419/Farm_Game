using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Item")]
public class ItemObject : ScriptableObject
{
    public string ObjName;              // 이름
    public Sprite uiDisplay;            // Ui용 아이콘
    public GameObject characterDisplay; // 장착템인 경우 장착 오브젝트
    public Crop CropData;                // Crop 정보

    public bool stackable;              // 아이템을 여러개 들고 있을 수 있는지 여부
    public ItemType type;               // 아이템 타입

    [TextArea(15, 20)] public string description; // 아이템 설명
    public Item data = new Item();                // 아이템 정보(이름, 아이디번호, 버프)

    // 아이템 생성
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }
}
