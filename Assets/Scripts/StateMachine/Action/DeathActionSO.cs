using UnityEngine;
using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(fileName = "DeathAction", menuName = "State Machines/Actions/Player Death")]
public class DeathActionSO : StateActionSO<DeathAction>
{
    [SerializeField]
    private StateAction.SpecificMoment _moment;
    public StateAction.SpecificMoment Moment => _moment;
    protected override StateAction CreateAction() => new DeathAction();
}

public class DeathAction : StateAction
{
    public override void OnStateEnter()
    {
        Debug.Log("사망");
    }

    public override void OnUpdate() { }
}