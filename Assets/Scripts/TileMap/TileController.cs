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
    [SerializeField] CropsManager cropsManager;
    private Movement _movement;

    public static UnityAction<Vector2> ClickEvent; // 마우스 이벤트

    public Vector2 mouseCurrentPosition;
    public Vector3Int gridPosition;

    [SerializeField] float maxDistance = 2f;

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        ClickEvent = (Vector2 mousePosition) => UseTool(mousePosition);
    }

    private void UseTool(Vector2 mousePosition) 
    {
        // 0. 인벤토리가 켜져 있을경우, 애니메이션이 동작하는 경우, 캐릭터가 움직일 경우 사용하지 않음
        if (GameManager.instance.tileManager.pissible) { return; }
        if (_movement.AnimTime) { return; }
        if (_movement.direction.sqrMagnitude > 0) { return; }

        // 1. 선택된 타일의 정보 추출
        mouseCurrentPosition = Mouse.current.position.ReadValue();
        gridPosition = GameManager.instance.tileManager.GetGridPosition(mouseCurrentPosition, true);
        TileBase tileName = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Interactable);

        // 2. 타일의 상태 종류 확인 - Interactable일 경우
        // 작물을 심을 수 있는 타일로 변경 -> 
        // 현재 도구가 괭이 일 경우 추가하기
        if (tileName.name == TileName.Interactable.ToString())
        {
            Vector2 charactorPosition = transform.position;
            Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition);
            GameManager.instance.tileManager.selctable = Vector2.Distance(charactorPosition, cameraPosition) < maxDistance; // 거리 확인
        }

        // 2. 타일의 상태 종류 확인 - Summer_Plowed일 경우
        // 작물을 심을 수 있는 타일로 변경
        else if (tileName.name == TileName.Summer_Plowed.ToString())
        {
            TileBase tilename = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Summer_Plowed);

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
