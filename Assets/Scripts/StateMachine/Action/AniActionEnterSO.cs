using UnityEngine;
using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "AniAction", menuName = "State Machines/Actions/AniEnter")]
public class AniActionEnterSO : StateActionSO
{
    protected override StateAction CreateAction() => new AniActionEnter();
}

public class AniActionEnter : StateAction
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