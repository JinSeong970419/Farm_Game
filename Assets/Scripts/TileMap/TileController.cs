using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    [SerializeField] TileMapReadController tileMapReadController;
    [SerializeField] CropsManager cropsManager;

    public static UnityAction<Vector2> ClickEvent;

    [SerializeField] float maxDistance = 2f;

    //Vector3Int selectedTilePosition;

    public static Dictionary<Vector2Int, TileData> fields;

    private void Awake()
    {
        ClickEvent = (Vector2 mousePosition) => UseTool(mousePosition);
    }

    private void Start()
    {
        fields = new Dictionary<Vector2Int, TileData>();
    }

    private void UseTool(Vector2 mousePosition) 
    {
        Vector2 mouseCurrentPosition = Mouse.current.position.ReadValue();
        Vector3Int gridPosition = tileMapReadController.GetGridPosition(mouseCurrentPosition, true);
        // 1. ���õ� Ÿ���� ���� ����
        TileBase tileName = GameManager.instance.tileManager.InteractableTileInfo(gridPosition);

        // 2. Ÿ���� ���� ���� Ȯ�� - Interactable�� ���
        // �۹��� ���� �� �ִ� Ÿ�Ϸ� ����
        if(tileName.name == "Interactable") // �� �� enum���� ����?
        {
            Vector2 charactorPosition = transform.position;
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition);
            bool selctable = Vector2.Distance(charactorPosition, cameraPosition) < maxDistance; // �Ÿ� Ȯ��

            // Plowed Ÿ�� ���� (Summer_Plowed)
            if (selctable) { GameManager.instance.tileManager.SetInteracted(gridPosition); }
        }

        // 2. Ÿ���� ���� ���� Ȯ�� - Summer_Plowed�� ���
        // �۹��� ���� �� �ִ� Ÿ�Ϸ� ����
        if (tileName.name == "Summer_Plowed")
        {
            GameManager.instance.tileManager.SetInteracted(gridPosition);
            cropsManager.SeedCrop(gridPosition, "corn");
        }
    }

}
