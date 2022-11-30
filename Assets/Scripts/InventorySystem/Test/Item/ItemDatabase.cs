using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Databas", menuName = "Inventory System/Items/Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemObject[] ItemObject;

    // ��ũ��Ʈ �ε� or �ν����� ���� ����� ��� ����
    public void OnValidate()
    {
        // ������ ���̽� ������� ������ Id ����
        for (int i = 0; i < ItemObject.Length; i++)
        {
            ItemObject[i].data.Id = i;
        }
    }
}
