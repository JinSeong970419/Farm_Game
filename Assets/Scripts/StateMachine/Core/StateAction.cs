using FarmGame.StateMachine.ScriptableObjects;

namespace FarmGame.StateMachine
{
	// Action 나타내는 개체
	public abstract class StateAction : IStateComponent
	{
		internal StateActionSO _originSO;

		protected StateActionSO OriginSO => _originSO;

		public abstract void OnUpdate();

		public virtual void Awake(StateMachine stateMachine) { }

		public virtual void OnStateEnter() { }
		public virtual void OnStateExit() { }

		// 사용 가능한 세 개의 "Moment" 중 하나로 실행.
		// 이 경우 StateAction은 모든 관련 함수를 구현해야 하며, 이 열거형을 조건으로 if 문을 사용하여 각 모멘트에서 동작할지 여부를 결정
		public enum SpecificMoment
		{
			OnStateEnter, OnStateExit, OnUpdate,
		}
	}
}
