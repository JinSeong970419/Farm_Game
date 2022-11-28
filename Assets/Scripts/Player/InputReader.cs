using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "InputReader", menuName = "Input System/InputReader")]
public class InputReader : ScriptableObject, PlayerControls.IPlayerActions
{
    public event UnityAction<Vector2> Movement = delegate { };
    public event UnityAction Inventory = delegate { };
    public event UnityAction<string> Keypad = delegate { };
    public event UnityAction MouseClick = delegate { };
    public event UnityAction SingKey = delegate { };

    private PlayerControls _playerCtrl;

    #region OnEnable & OnDisable
    private void OnEnable()
    {
        if (_playerCtrl == null)
        {
            _playerCtrl = new PlayerControls();

            // SetCallbacks 콜백을 받기 위한 함수
            _playerCtrl.Player.SetCallbacks(this);
            _playerCtrl.Enable();
        }

    }

    private void OnDisable()
    {
        _playerCtrl.Player.Disable();
    }
    #endregion

    public void OnMove(InputAction.CallbackContext context) { Movement.Invoke(context.ReadValue<Vector2>()); }

    public void OnInventory(InputAction.CallbackContext context) { Inventory.Invoke(); }

    public void OnMenuBar(InputAction.CallbackContext context) { if (context.started) { Keypad.Invoke(context.control.name); } }

    public void OnMouse(InputAction.CallbackContext context) { if (context.started) { MouseClick.Invoke(); } }

    public void OnSingKey(InputAction.CallbackContext context) { if (context.started || context.canceled) { SingKey.Invoke(); } }
}
