using UnityEngine;
using FarmGame.StateMachine;
using FarmGame.StateMachine.ScriptableObjects;

[CreateAssetMenu(menuName = "State Machines/Conditions/Water")]
public class IsWaterSO : StateConditionSO<IsWaterConditon>
{
}

public class IsWaterConditon : Condition
{
    protected override bool Statement()
    {
        return GameManager.instance.tileManager.waterble; 
    }
}
