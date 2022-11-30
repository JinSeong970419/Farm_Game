using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item Databas", menuName = "Inventory System/Items/Database")]
public class ItemDatabase : ScriptableObject
{
    public ItemObject[] ItemObject;

    // 스크립트 로딩 or 인스펙터 값이 변경될 경우 실행
    public void OnValidate()
    {
        // 데이터 베이스 순서대로 아이템 Id 지정
        for (int i = 0; i < ItemObject.Length; i++)
        {
            ItemObject[i].data.Id = i;
        }
    }
}
