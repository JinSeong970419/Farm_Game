using UnityEngine;
using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HopAction", menuName = "State Machines/Actions/HopActionExit")]
public class HopActionSO : StateActionSO
{
	[SerializeField]
	private StateAction.SpecificMoment _moment;
	public StateAction.SpecificMoment Moment => _moment;
	protected override StateAction CreateAction() => new HopAction();
}

public class HopAction : StateAction
{
	private TileController _TileCtroller;
	public override void Awake(StateMachine stateMachine)
    {
		_TileCtroller = stateMachine.GetComponent<TileController>();
	}

	public override void OnStateEnter() { }
	public override void OnUpdate()	{ }

	public override void OnStateExit() 
	{
		GameManager.instance.tileManager.SetInteracted(_TileCtroller.gridPosition, TileName.Summer_Plowed);
	}
}