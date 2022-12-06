using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "HopAction", menuName = "State Machines/Actions/HopActionEnter")]
public class HopActionEnterSO : StateActionSO
{
    protected override StateAction CreateAction() => new HopActionEnter();
}

public class HopActionEnter : StateAction
{
    Movement _movement;
    public override void Awake(StateMachine stateMachine)
    {
        _movement = stateMachine.GetComponent<Movement>();
    }
    public override void OnStateEnter()
	{
        _movement.AnimTime = true;
    }
	public override void OnUpdate() { }

	public override void OnStateExit() { }
}