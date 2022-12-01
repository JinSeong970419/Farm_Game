using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public InputReader _inputReader;
    public InventoryObject inventory;
    private BeasItem item;

    private void Awake()
    {
        //inventory = GetComponent<InventoryManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        item = collision.GetComponent<BeasItem>();
        Destroy(collision.gameObject);
        inventory.AddItem(new Item(item.item), 1);
    }

    //// 아이템 버리기
    //public void DropItem(Item item)
    //{
    //    Vector3 spawnLocation = transform.position;

    //    Vector3 spawnOffset = Random.insideUnitCircle * 1.25f; 

    //    //Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);

    //    //droppedItem.rb2d.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
    //}

    //// 아이템  
    //public void DropItem(Item item, int numToDrop)
    //{
    //    for (int i = 0; i < numToDrop; i++)
    //    {
    //        DropItem(item);
    //    }
    //}
}
