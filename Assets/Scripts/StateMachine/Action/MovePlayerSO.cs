using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "MovementVector", menuName = "State Machines/Actions/Movement Vector")]
public class MovePlayerSO : StateActionSO<MovePlayer> { }

public class MovePlayer : StateAction
{
    public Movement _movement;

    public override void Awake(StateMachine stateMachine)
    {
        _movement = stateMachine.GetComponent<Movement>();
    }

    public override void OnUpdate()
    {
        _movement.gameObject.transform.position += _movement.movementVector * Time.deltaTime;
    }
}