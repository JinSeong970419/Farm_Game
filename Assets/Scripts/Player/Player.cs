using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //public InputReader _inputReader;
    public InventoryManager inventory;

    private void Awake()
    {
        inventory = GetComponent<InventoryManager>();
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    Vector3Int position = new Vector3Int((int)transform.position.x, (int)transform.position.y, 0);
        //    Debug.Log($"Player ��ġ : {position}");
        //    if (GameManager.instance.tileManager.IsInteractable(position))
        //    {
        //        Debug.Log("��� ���� Ÿ��");
        //        GameManager.instance.tileManager.SetInteracted(position);
        //    }
        //}
    }

    // ������ ������
    public void DropItem(Item item)
    {
        Vector3 spawnLocation = transform.position;

        Vector3 spawnOffset = Random.insideUnitCircle * 1.25f; 

        Item droppedItem = Instantiate(item, spawnLocation + spawnOffset, Quaternion.identity);

        droppedItem.rb2d.AddForce(spawnOffset * .2f, ForceMode2D.Impulse);
    }

    // ������  
    public void DropItem(Item item, int numToDrop)
    {
        for (int i = 0; i < numToDrop; i++)
        {
            DropItem(item);
        }
    }
}
