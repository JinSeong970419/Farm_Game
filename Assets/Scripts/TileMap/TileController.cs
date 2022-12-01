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
        // 0. 인벤토리가 켜져 있을경우 사용하지 않음
        if (GameManager.instance.tileManager.pissible) { return; }

        // 1. 선택된 타일의 정보 추출
        Vector2 mouseCurrentPosition = Mouse.current.position.ReadValue();
        Vector3Int gridPosition = GameManager.instance.tileManager.GetGridPosition(mouseCurrentPosition, true);
        TileBase tileName = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Interactable);

        // 타일 이름 테스트
        // Debug.Log(tileName.name);

        // 2. 타일의 상태 종류 확인 - Interactable일 경우
        // 작물을 심을 수 있는 타일로 변경
        if (tileName.name == TileName.Interactable.ToString())
        {
            Vector2 charactorPosition = transform.position;
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition);
            bool selctable = Vector2.Distance(charactorPosition, cameraPosition) < maxDistance; // 거리 확인

            // Plowed 타일 변경 (Summer_Plowed)
            if (selctable) 
            {
                GameManager.instance.tileManager.SetInteracted(gridPosition); 
            }
        }

        // 2. 타일의 상태 종류 확인 - Summer_Plowed일 경우
        // 작물을 심을 수 있는 타일로 변경
        else if (tileName.name == TileName.Summer_Plowed.ToString())
        {
            TileBase tilename = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Summer_Plowed);
            //Debug.Log($"타일 이름 : {tilename}");

            // 임시
            // 1. 작물이 심어져 있지 않은 경우
            if(tilename == null)
            {
                //GameManager.instance.tileManager.SetInteracted(gridPosition);
                cropsManager.SeedCrop(gridPosition, Cropseed.corn);
            }
            
            // 2. 작물이 심어져 있는 경우
            else
            {
                // 1. 상태 확인

                // 2. 다 자라지 않았을 경우
                cropsManager.Water(gridPosition);
                tilename = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Summer_Plowed);

                // 3. 농작물이 모두 성장 했을 경우
            }

        }
    }

}
