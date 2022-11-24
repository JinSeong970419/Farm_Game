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

    // Interactable 타일맵 정보 확인
    public TileBase TileInfo(Vector3Int position, int state) // int를 enum 형태로 바꾸기
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

    // 괭이질 가능 여부 확인 ? 사용가능 땅 확인 여부
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
}
