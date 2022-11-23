using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    [SerializeField] TileMapReadController tileMapReadController;
    public static UnityAction<Vector2> ClickEvent;

    [SerializeField] float maxDistance = 2f;

    //Vector3Int selectedTilePosition;

    public static Dictionary<Vector2Int, TileData> fields;

    private void Awake()
    {
        ClickEvent = (Vector2 mousePosition) => UseTool(mousePosition);
    }

    private void UseTool(Vector2 mousePosition) 
    {
        Vector2 mouseCurrentPosition = Mouse.current.position.ReadValue();
        // 변경 가능 여부 확인
        if (GameManager.instance.tileManager.IsInteractable(tileMapReadController.GetGridPosition(mouseCurrentPosition, true)))
        {
            Vector2 charactorPosition = transform.position;
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition);
            bool selctable = Vector2.Distance(charactorPosition, cameraPosition) < maxDistance; // 거리 확인

            // Plowed 타일 변경
            if (selctable)
            {
                GameManager.instance.tileManager.SetInteracted(tileMapReadController.GetGridPosition(mouseCurrentPosition, true));
            }
        }
    }

    // 타일 선택
    //private void SelectTile()
    //{
    //    selectedTilePosition = tileMapReadController.GetGridPosition(Mouse.current.position.ReadValue(), true);
    //    TileBase tileBase = tileMapReadController.GetTileBase(selectedTilePosition);

    //    try
    //    {
    //        TileData tileData = tileMapReadController.GetTileData(tileBase);
    //        if (!(tileData is null))
    //        {
    //            if (!fields.ContainsKey((Vector2Int)selectedTilePosition))
    //            {
    //                fields.Add((Vector2Int)selectedTilePosition, tileData);
    //            }
    //            else
    //            {
    //                fields[(Vector2Int)selectedTilePosition] = tileData;
    //            }
    //        }
    //    }
    //    catch
    //    {
    //        Debug.Log("Catch문 확인");
    //        return;
    //    }
    //}
}
