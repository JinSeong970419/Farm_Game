using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileMapReadController : MonoBehaviour
{
    [SerializeField] Tilemap tilemap;          // Ground Ÿ��
    [SerializeField] List<TileData> tileDatas; // Ÿ�� ���� ����
    Dictionary<TileBase, TileData> dataFromTiles;

    private void Start()
    {
        dataFromTiles = new Dictionary<TileBase, TileData>();

        foreach (TileData tileData in tileDatas)
        {
            foreach (TileBase tile in tileData.tiles)
            {
                Debug.Log($"Ÿ�� ���� Ȯ�� : {tileData.tiles}");
                dataFromTiles.Add(tile, tileData);
            }
        }
    }

    // Grid ��ġ(��ǥ) Ȯ��
    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;
        worldPosition = mousePosition ? Camera.main.ScreenToWorldPoint(position) : position; // ���콺 ��ġ�� vector3�� ������

        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition); // Vector3�� Vector3Int�� ����

        return gridPosition;
    }

    // ǥ�õ� ��ǥ Ÿ�� ��������
    public TileBase GetTileBase(Vector3Int gridPosition)
    {
        TileBase tile = tilemap.GetTile(gridPosition);
        return tile;
    }

    // Ÿ�� ������ �����ص� dictionary ��ȯ
    public TileData GetTileData(TileBase tilebase)
    {
        return dataFromTiles[tilebase];
    }
}