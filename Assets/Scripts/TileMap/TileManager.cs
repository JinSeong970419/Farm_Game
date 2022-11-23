using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{
    [SerializeField] private Tilemap interactableMap;

    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile interactedTile;

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

    // 상호작용 가능 여부 확인
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
