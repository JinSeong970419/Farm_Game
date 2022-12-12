using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class TileController : MonoBehaviour
{
    [SerializeField] private CropsManager cropsManager;
    private Movement _movement;
    private ItemObject itemObject;
    private TileBase tileName;
    private Crop crops;

    private Vector2 mouseCurrentPosition;
    [HideInInspector] public Vector3Int gridPosition;

    // 툴 상호작용 거리
    [SerializeField] float maxDistance = 2f;

    public static UnityAction<Vector2> ClickEvent; // 마우스 이벤트

    private void Awake()
    {
        _movement = GetComponent<Movement>();
        ClickEvent = (Vector2 mousePosition) => UseTool(mousePosition);
    }

    private void UseTool(Vector2 mousePosition) 
    {
        // 0. 인벤토리가 켜져 있을경우, 애니메이션이 동작하는 경우, 캐릭터가 움직일 경우 사용하지 않음
        if (GameManager.instance.tileManager.pissible || _movement.AnimTime || _movement.direction.sqrMagnitude > 0) { return; }

        // 1-1. 선택된 타일의 정보 추출
        mouseCurrentPosition = Mouse.current.position.ReadValue();
        gridPosition = GameManager.instance.tileManager.GetGridPosition(mouseCurrentPosition, true);
        tileName = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Interactable);

        // 1-2. 선택된 도구 확인
        itemObject = _movement.ToolbarUI.slotsOnInterface[MouseData.selectBar].GetItemObject();
        if (itemObject == null) return; // 선택이 안됐을 경우 리턴

        // 2. 타일의 상태 종류 확인
        // 작물을 심을 수 있는 타일로 변경
        if (tileName.name == TileName.Interactable.ToString())
        {
            if(itemObject.type == ItemType.Hop)
            {
                Vector2 charactorPosition = transform.position;
                Vector2 cameraPosition = Camera.main.ScreenToWorldPoint(mouseCurrentPosition);
                GameManager.instance.tileManager.selctable = Vector2.Distance(charactorPosition, cameraPosition) < maxDistance; // 거리 확인
            }
        }

        // 2. 타일의 상태 종류 확인 - Summer_Plowed일 경우
        // 작물을 심을 수 있는 타일로 변경
        else if (tileName.name == TileName.Summer_Plowed.ToString())
        {
            tileName = GameManager.instance.tileManager.TileInfo(gridPosition, TileName.Summer_Plowed);

            // 1. 작물이 심어져 있지 않은 경우
            if(tileName == null)
            {
                if (itemObject.type == ItemType.Seed)
                    cropsManager.SeedCrop(gridPosition, itemObject.CropData);
            }
            
            // 2. 작물이 심어져 있는 경우
            else
            {
                crops = GameManager.instance.cropManager.crops[gridPosition];
                // 2-1. 작물이 자라지 않았을 경우
                if (crops.stateIndex < crops.state.Length - 1)
                {
                    // 물주기
                    if (itemObject.type == ItemType.water)
                        GameManager.instance.tileManager.waterble = true;
                }
                // 2-2. 작물이 모두 성장 했을 경우
                else { cropsManager.Harvest(crops.position); }
            }
        }
    }
}
