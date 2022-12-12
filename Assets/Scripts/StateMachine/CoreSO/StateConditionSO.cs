using System.Collections.Generic;
using UnityEngine;

namespace FarmGame.StateMachine.ScriptableObjects
{
	public abstract class StateConditionSO : ScriptableObject
	{
		// 새 사용자 지정 참조 또는 내부의 기존 사용자 지정을 사용
		internal StateCondition GetCondition(StateMachine stateMachine, bool expectedResult, Dictionary<ScriptableObject, object> createdInstances)
		{
			if (!createdInstances.TryGetValue(this, out var obj))
			{
				var condition = CreateCondition();
				condition._originSO = this;
				createdInstances.Add(this, condition);
				condition.Awake(stateMachine);

				obj = condition;
			}

			return new StateCondition(stateMachine, (Condition)obj, expectedResult);
		}
		protected abstract Condition CreateCondition();
	}


	public abstract class StateConditionSO<T> : StateConditionSO where T : Condition, new()
	{
		protected override Condition CreateCondition() => new T();
	}
}
