using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;
using UnityEngine;

[CreateAssetMenu(fileName = "MoveAction", menuName = "State Machines/Actions/MoveActionExit")]
public class MoveActionExitSO : StateActionSO
{
	[SerializeField]
	private StateAction.SpecificMoment _moment;
	public StateAction.SpecificMoment Moment => _moment;
	protected override StateAction CreateAction() => new MoveActionExit();
}

public class MoveActionExit : StateAction
{
	private Movement _movement;
	public override void Awake(StateMachine stateMachine)
	{
		_movement = stateMachine.GetComponent<Movement>();
	}

	public override void OnStateEnter()
	{
	}
	public override void OnUpdate() { }

	public override void OnStateExit()
	{
		_movement.direction = Vector3.zero;
	}
}