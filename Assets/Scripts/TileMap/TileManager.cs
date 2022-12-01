using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] Tilemap Groundtilemap;    // Ground 타일
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap cropsTileMap;

    [SerializeField] List<TileData> tileDatas; // 타일 정보 변수
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;

    Dictionary<List<TileBase>, TileData> dataFromTiles;

    public bool pissible = true;
    TileBase tile;
    /*-------------------------------------------------------*/

    private void Awake()
    {
        dataFromTiles = new Dictionary<List<TileBase>, TileData>();
    }

    

    /*-------------------------------------------------------*/

    void Start()
    {
        foreach (TileData tileData in tileDatas)
        {
            dataFromTiles.Add(tileData.tiles, tileData);
        }

        // 농사 가능한 지역 찾아오기
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            // 이름이 Interactable_Visible 농사 가능지역 검색 후 타일 정보 변경
            if (tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    // Grid 위치(좌표) 확인
    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;
        worldPosition = mousePosition ? Camera.main.ScreenToWorldPoint(position) : position; // 마우스 위치를 vector3로 가져옴

        Vector3Int gridPosition = Groundtilemap.WorldToCell(worldPosition); // Vector3를 Vector3Int로 변경

        return gridPosition;
    }

    // 타일맵 정보 확인
    public TileBase TileInfo(Vector3Int position, TileName tiles)
    {
        switch (tiles)
        {
            case TileName.Interactable:
                tile = interactableMap.GetTile(position);
                break;
            case TileName.Summer_Plowed:
                tile = cropsTileMap.GetTile(position);
                break;
        }
        return tile;
    }

    // 사용 가능 땅 여부 확인
    public bool IsInteractable(Vector3Int position)
    {
        TileBase tile = interactableMap.GetTile(position);
        if(tile != null)
        {
            if(tile.name == "Interactable")
            {
                return true;
            }
        }
        return false;
    }

    // 괭이 완료 지역 표시
    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);
    }

    public void SetInteracted(Vector3Int position, Tile tile)
    {
        interactableMap.SetTile(position, tile);
    }
}
