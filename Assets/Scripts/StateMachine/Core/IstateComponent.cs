namespace FarmGame.StateMachine
{
	interface IStateComponent
	{
		// 상태 진입 시 호출
		void OnStateEnter();

		// 상태 종료 시 호출
		void OnStateExit();
	}
}
