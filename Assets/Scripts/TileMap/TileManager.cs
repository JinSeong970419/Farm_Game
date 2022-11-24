using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap cropsTileMap;

    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;

    TileBase tile;
    void Start()
    {
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

    // Interactable Ÿ�ϸ� ���� Ȯ��
    public TileBase TileInfo(Vector3Int position, int state) // int�� enum ���·� �ٲٱ�
    {
        switch (state)
        {
            case 0:
                tile = interactableMap.GetTile(position);
                break;
            case 1:
                tile = cropsTileMap.GetTile(position);
                break;
        }
        return tile;
    }

    // ������ ���� ���� Ȯ�� ? ��밡�� �� Ȯ�� ����
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
}
