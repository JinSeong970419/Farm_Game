using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;          // Ground 타일
    [SerializeField] List<TileData> tileDatas; // 타일 정보 변수
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)
            {
                Debug.Log($"타일 정보 확인 : {tileData.tiles}");
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    // Grid 위치(좌표) 확인
    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;
        worldPosition = mousePosition ? Camera.main.ScreenToWorldPoint(position) : position; // 마우스 위치를 vector3로 가져옴

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition); // Vector3를 Vector3Int로 변경

        return gridPosition;
    }

    // 표시된 좌표 타일 가져오기
    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        TileBase tile = tilemap.GetTile(gridPosition);
        return tile;
    }

    // 타일 정보를 저장해둔 dictionary 반환
    public TileData GetTileData(TileBase tilebase)
    {
        return dataFromTiles[tilebase];
    }
}