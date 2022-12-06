using UnityEngine;
using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Plant")]
public class IsPlantSO : StateConditionSO<IsPlantCondition>
{
}

public class IsPlantCondition : Condition
{
    protected override bool Statement()
    {
        return GameManager.instance.tileManager.selctable;
    }
}
