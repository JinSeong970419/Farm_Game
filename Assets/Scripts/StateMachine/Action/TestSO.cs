using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "StopMovementAction", menuName = "State Machines/Actions/Stop Movement")]
public class TestSO : StateActionSO
{
	[SerializeField]
	private StateAction.SpecificMoment _moment;
	public StateAction.SpecificMoment Moment => _moment;
	protected override StateAction CreateAction() => new TestS();
}

public class TestS : StateAction
{
	private Movement _protagonist;

	public override void Awake(StateMachine stateMachine)
    {
		_protagonist = stateMachine.GetComponent<Movement>();
	}

    private new TestSO OriginSO => (TestSO)base.OriginSO;

	public override void OnUpdate()
	{
		if (OriginSO.Moment == SpecificMoment.OnUpdate)
			_protagonist.movementVector = Vector3.zero;
	}
	public override void OnStateEnter()
	{
		if (OriginSO.Moment == SpecificMoment.OnStateEnter)
			_protagonist.movementVector = Vector3.zero;
	}

	public override void OnStateExit()
	{
		if (OriginSO.Moment == SpecificMoment.OnStateExit)
			_protagonist.movementVector = Vector3.zero;
	}
}