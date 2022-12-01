using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public enum TileName
{
    Interactable,
    Summer_Plowed,
    Wetground,
    Dryground
}

public enum Cropseed
{
    corn
}

public class TileController : MonoBehaviour
{
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] CropsManager cropsManager;

    public static UnityAction<Vector2> ClickEvent;

    [SerializeField] float maxDistance = 2f;

    //Vector3Int selectedTilePosition;

    //public static Dictionary<Vector2Int, TileData> fields;


    private void Awake()
    {
        ClickEvent = (Vector2 mousePosition) => UseTool(mousePosition);
    }

    private void Start()
    {
        //fields = new Dictionary<Vector2Int, TileData>();
    }

    private void UseTool(Vector2 mousePosition) 
    {
        // 0. �κ��丮�� ���� ������� ������� ����
        if (GameManager.instance.tileManager.pissible) { return; }

        // 1. ���õ� Ÿ���� ���� ����
        Vector2 mouseCurrentPosition = Mouse.current.position.ReadValue();
        Vector3Int gridPosition = GameManager.instance.tileManager.GetGridPosition(mouseCurrentPosition, true);
        TileBase tileName = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Interactable);

        // Ÿ�� �̸� �׽�Ʈ
        // Debug.Log(tileName.name);

        // 2. Ÿ���� ���� ���� Ȯ�� - Interactable�� ���
        // �۹��� ���� �� �ִ� Ÿ�Ϸ� ����
        if (tileName.name == TileName.Interactable.ToString())
        {
            Vector2 charactorPosition = transform.position;
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition);
            bool selctable = Vector2.Distance(charactorPosition, cameraPosition) < maxDistance; // �Ÿ� Ȯ��

            // Plowed Ÿ�� ���� (Summer_Plowed)
            if (selctable) 
            {
                GameManager.instance.tileManager.SetInteracted(gridPosition); 
            }
        }

        // 2. Ÿ���� ���� ���� Ȯ�� - Summer_Plowed�� ���
        // �۹��� ���� �� �ִ� Ÿ�Ϸ� ����
        else if (tileName.name == TileName.Summer_Plowed.ToString())
        {
            TileBase tilename = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Summer_Plowed);
            //Debug.Log($"Ÿ�� �̸� : {tilename}");

            // �ӽ�
            // 1. �۹��� �ɾ��� ���� ���� ���
            if(tilename == null)
            {
                //GameManager.instance.tileManager.SetInteracted(gridPosition);
                cropsManager.SeedCrop(gridPosition, Cropseed.corn);
            }
            
            // 2. �۹��� �ɾ��� �ִ� ���
            else
            {
                // 1. ���� Ȯ��

                // 2. �� �ڶ��� �ʾ��� ���
                cropsManager.Water(gridPosition);
                tilename = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Summer_Plowed);

                // 3. ���۹��� ��� ���� ���� ���
            }

        }
    }

}
