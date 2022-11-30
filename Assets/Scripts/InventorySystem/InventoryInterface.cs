using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class InventoryInterface : UserInterface
{
    private void OnEnable()
    {
        Debug.Log("dqwdqwdq");
        CreateSlots();
    }

    public override void CreateSlots()
    {
        for (int i = 0; i < inventory.GetSlots.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            Debug.Log(obj.name);
            obj.name = inventoryPrefab.name + i.ToString();
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
            //inventory.GetSlots[i].slotDisplay = obj;
            slotsOnInterface.Add(obj, inventory.GetSlots[i]);
        }
    }

    public override Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + (X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN)), Y_START + (-Y_SPACE_BWTWEEN_ITEMS * (i / NUMBER_OF_COLUMN)), 0f);
    }
}
