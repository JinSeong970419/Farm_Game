using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Inventory System/Items/Item")]
public class ItemObject : ScriptableObject
{
    public string ObjName;        //3D ������Ʈ�� �ʵ忡 ��ȯ�� ������Ʈ
    public Sprite uiDisplay;            // Ui�� ������
    public GameObject characterDisplay; // �������� ��� ���� ������Ʈ
    public bool stackable;              // �������� ������ ��� ���� �� �ִ��� ����
    public ItemType type;

    [TextArea(15, 20)] public string description; // ������ ����
    public Item data = new Item();                // ������ ����(�̸�, ���̵��ȣ, ����)

    //public List<string> boneNames = new List<string>(); // Player ���� ����

    // ������ ����
    public Item CreateItem()
    {
        Item newItem = new Item(this);
        return newItem;
    }

    //private void OnValidate()
    //{
        //boneNames.Clear();
        //if (characterDisplay == null) { return; }  // ���� ������Ʈ ���� Ȯ��

        //if (!characterDisplay.GetComponent<SkinnedMeshRenderer>()) { return; } // ������Ʈ ���� ���� Ȯ��

        //var renderer = characterDisplay.GetComponent<SkinnedMeshRenderer>();
        //var bones = renderer.bones;

        // �÷��̾� ������ ������ ����
        //foreach (var t in bones)
        //{
        //    boneNames.Add(t.name);
        //}
    //}
}
