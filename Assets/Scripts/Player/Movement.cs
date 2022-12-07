using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어 이동 제어 관련 -> 제어 이름변경하기
public class Movement : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [HideInInspector] public bool AnimTime; // 애니메이션 완료 여부 확인

    [HideInInspector] public Vector2 _inputVector;
    [HideInInspector] public Vector3 direction;
    [HideInInspector] public Vector3 movementVector;

    [SerializeField] public InventoryInterface inventoryUI;
    [SerializeField] public ToobarInterface ToolbarUI;

    public int inventorySize;
    public int toolbarSize;
    private void OnEnable()
    {
        _inputReader.Movement += OnMove;
        _inputReader.Keypad += OnKeyPad;
        _inputReader.MouseClick += OnClick;
        _inputReader.SingKey += OnSingle;
        _inputReader.Inventory += OnInventory;
    }

    private void OnDisable()
    {
        _inputReader.Movement -= OnMove;
        _inputReader.Keypad -= OnKeyPad;
        _inputReader.MouseClick -= OnClick;
        _inputReader.SingKey -= OnSingle;
        _inputReader.Inventory -= OnInventory;
    }

    private void Awake()
    {
        inventoryUI.InitializeInventoryUI(inventorySize);
        ToolbarUI.InitializeInventoryUI(toolbarSize);
    }

    void Update()
    {
        if(!AnimTime)
            direction = new Vector3(_inputVector.x, _inputVector.y);
    }

    private void OnMove(Vector2 movement) { _inputVector = movement; }
    private void OnKeyPad(string number) { Toolbar_UI.checAlphaNumbericKeys(int.Parse(number));}
    private void OnClick() { TileController.ClickEvent(Mouse.current.position.ReadValue()); }
    private void OnSingle() {
        /*UI_Manager.SingleOnOff(); */}

    // Inventory Ui On/Off
    private void OnInventory()
    {
        if (inventoryUI.isActiveAndEnabled == false)
        {
            inventoryUI.Show();
        }
        else
        {
            inventoryUI.Hide();
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        BaesItem item = collision.GetComponent<BaesItem>();
        if (item != null)
        {
            Destroy(collision.gameObject);
            inventoryUI.inventory.AddItem(new Item(item.item), 1);
        }
    }
}