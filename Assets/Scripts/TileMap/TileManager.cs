using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] Tilemap Groundtilemap;    // Ground Ÿ��
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap cropsTileMap;

    [SerializeField] List<TileData> tileDatas; // Ÿ�� ���� ����
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

        // ��� ������ ���� ã�ƿ���
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        {
            TileBase tile = interactableMap.GetTile(position);

            // �̸��� Interactable_Visible ��� �������� �˻� �� Ÿ�� ���� ����
            if (tile != null && tile.name == "Interactable_Visible")
            {
                interactableMap.SetTile(position, hiddenInteractableTile);
            }
        }
    }

    // Grid ��ġ(��ǥ) Ȯ��
    public Vector3Int GetGridPosition(Vector2 position, bool mousePosition)
    {
        Vector3 worldPosition;
        worldPosition = mousePosition ? Camera.main.ScreenToWorldPoint(position) : position; // ���콺 ��ġ�� vector3�� ������

        Vector3Int gridPosition = Groundtilemap.WorldToCell(worldPosition); // Vector3�� Vector3Int�� ����

        return gridPosition;
    }

    // Ÿ�ϸ� ���� Ȯ��
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

    // ��� ���� �� ���� Ȯ��
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

    // ���� �Ϸ� ���� ǥ��
    public void SetInteracted(Vector3Int position)
    {
        interactableMap.SetTile(position, interactedTile);
    }

    public void SetInteracted(Vector3Int position, Tile tile)
    {
        interactableMap.SetTile(position, tile);
    }
}
