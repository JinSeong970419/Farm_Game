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
        // 1. 선택된 타일의 정보 추출
        TileBase tileName = GameManager.instance.tileManager.InteractableTileInfo(gridPosition);

        // 2. 타일의 상태 종류 확인 - Interactable일 경우
        // 작물을 심을 수 있는 타일로 변경
        if(tileName.name == "Interactable") // 추 후 enum으로 변경?
        {
            Vector2 charactorPosition = transform.position;
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition);
            bool selctable = Vector2.Distance(charactorPosition, cameraPosition) < maxDistance; // 거리 확인

            // Plowed 타일 변경 (Summer_Plowed)
            if (selctable) { GameManager.instance.tileManager.SetInteracted(gridPosition); }
        }

        // 2. 타일의 상태 종류 확인 - Summer_Plowed일 경우
        // 작물을 심을 수 있는 타일로 변경
        if (tileName.name == "Summer_Plowed")
        {
            GameManager.instance.tileManager.SetInteracted(gridPosition);
            cropsManager.SeedCrop(gridPosition, "corn");
        }
    }

}
