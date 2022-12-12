using UnityEngine;
using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Death")]
public class IsDeathSO : StateConditionSO<IsDeathCondition>
{
    public bool TestNeverDie = false;
}

public class IsDeathCondition : Condition
{
    private PlayerHealth _Health;
    private IsDeathSO originSO => (IsDeathSO)OriginSO;
    public override void Awake(StateMachine stateMachine)
    {
        _Health = stateMachine.GetComponent<PlayerHealth>();
    }

    protected override bool Statement()
    {
        bool CheckHP = false;
        if (_Health.HP == 0 && !originSO.TestNeverDie)
            CheckHP = true;
        return CheckHP;
    }
}
