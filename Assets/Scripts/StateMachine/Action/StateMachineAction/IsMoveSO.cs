using UnityEngine;
using UOP1.StateMachine;
using UOP1.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Move")]
public class IsMoveSO : StateConditionSO<IsMoveCondition>
{
    public float treshold = 0.02f;
}

public class IsMoveCondition : Condition
{
    private Movement _MoveMent;
    private IsMoveSO originSO => (IsMoveSO)OriginSO;

    public override void Awake(StateMachine stateMachine)
    {
        _MoveMent = stateMachine.GetComponent<Movement>();
    }

    protected override bool Statement()
    {
        Vector3 direction = _MoveMent.direction;
        direction.z = 0f;
        return direction.sqrMagnitude > originSO.treshold;
    }
}