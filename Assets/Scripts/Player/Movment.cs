using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// 플레이어 이동 제어 관련 -> 제어 이름변경하기
public class Movment : MonoBehaviour
{
    public float speed;

    public Animator animator;

    public InputReader _inputReader;
    private Vector2 _inputVector;

    private Vector3 direction;

    private void OnEnable()
    {
        _inputReader.Movement += OnMove;
        _inputReader.Inventory += OnInventory;
        _inputReader.Keypad += OnKeyPad;
        _inputReader.MouseClick += OnClick;
    }

    private void OnDisable()
    {
        _inputReader.Movement -= OnMove;
        _inputReader.Inventory -= OnInventory;
        _inputReader.Keypad -= OnKeyPad;
        _inputReader.MouseClick -= OnClick;
    }

    void Update()
    {
        direction = new Vector3(_inputVector.x, _inputVector.y);

        AnimateMovement(direction);
    }

    private void FixedUpdate()
    {
        transform.position += direction * speed* Time.deltaTime;
    }

    private void OnMove(Vector2 movement) { _inputVector = movement; }
    private void OnInventory() { UI_Manager.InventoryOnOff(); }
    private void OnKeyPad(string number) { Toolbar_UI.checAlphaNumbericKeys(number); }
    private void OnClick() { TileController.ClickEvent(Mouse.current.position.ReadValue()); }

    void AnimateMovement(Vector3 direction)
    {
        if(animator != null)
        {
            if(direction.magnitude > 0)
            {
                animator.SetBool("isMoving", true);

                animator.SetFloat("horizontal", direction.x);
                animator.SetFloat("vertical", direction.y);
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
        }
    }
}