using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Item")]
public class ItemObject : ScriptableObject
{
    public string ObjName;        //3D 오브젝트로 필드에 소환될 오브젝트
    public Sprite uiDisplay;            // Ui용 아이콘
    public GameObject characterDisplay; // 장착템인 경우 장착 오브젝트
    public bool stackable;              // 아이템을 여러개 들고 있을 수 있는지 여부
    public ItemType type;

    [TextArea(15, 20)] public string description; // 아이템 설명
    public Item data = new Item();                // 아이템 정보(이름, 아이디번호, 버프)

    //public List<string> boneNames = new List<string>(); // Player 구조 저장

    // 아이템 생성
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    //private void OnValidate()
    //{
        //boneNames.Clear();
        //if (characterDisplay == null) { return; }  // 장착 오브젝트 여후 확인

        //if (!characterDisplay.GetComponent<SkinnedMeshRenderer>()) { return; } // 컴포넌트 보유 여부 확인

        //var renderer = characterDisplay.GetComponent<SkinnedMeshRenderer>();
        //var bones = renderer.bones;

        // 플레이어 구조에 랜더러 장착
        //foreach (var t in bones)
        //{
        //    boneNames.Add(t.name);
        //}
    //}
}
