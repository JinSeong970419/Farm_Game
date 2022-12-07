using UnityEngine;
using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "WaterActionExit", menuName = "State Machines/Actions/WaterActionExit")]
public class WaterActionSO : StateActionSO
{
	[SerializeField]
	private StateAction.SpecificMoment _moment;
	public StateAction.SpecificMoment Moment => _moment;
	protected override StateAction CreateAction() => new WaterAction();
}

public class WaterAction : StateAction
{
	private TileController _TileCtroller;
	public override void Awake(StateMachine stateMachine)
	{
		_TileCtroller = stateMachine.GetComponent<TileController>();
	}

	public override void OnStateEnter()	{ }
	public override void OnUpdate() { }

	public override void OnStateExit()
	{
		GameManager.instance.cropManager.Water(_TileCtroller.gridPosition);
	}
}