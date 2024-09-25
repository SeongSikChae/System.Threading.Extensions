using Quartz;

namespace System.Threading.Tasks
{
	/// <summary>
	/// TaskScheduler 인터페이스
	/// </summary>
	public interface ITaskScheduler
	{
		/// <summary>
		/// TimeSpan Interval 주기로 실행되는 태스크 스케줄 추가
		/// </summary>
		/// <param name="id"></param>
		/// <param name="task"></param>
		/// <param name="interval"></param>
		void AddTask(string id, ITask task, TimeSpan interval);

		/// <summary>
		/// Cron Expression 주기로 실행되는 태스크 스케줄 추가
		/// </summary>
		/// <param name="id"></param>
		/// <param name="task"></param>
		/// <param name="expression"></param>
		void AddTask(string id, ITask task, CronExpression expression);

		/// <summary>
		/// 특정 ID에 맞는 태스크 스케줄이 존재하는지 여부
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		bool ContainTask(string id);

		/// <summary>
		/// 특정 ID에 맞는 태스크 스케줄의 다음 실행 시간을 가져온다.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		DateTime? NextExecutionTime(string id);

		/// <summary>
		/// 특정 ID에 맞는 대기중인 태스크를 깨웁니다.
		/// </summary>
		/// <param name="id"></param>
		void WakeUp(string id);

		/// <summary>
		/// 특정 ID에 맞는 태스크를 스케줄에서 삭제합니다.
		/// </summary>
		/// <param name="id"></param>
		void RemoveTask(string id);

		/// <summary>
		/// 모든 태스크 스케줄이 중지되기를 기다립니다.
		/// </summary>
		void WaitForShutdown();
	}
}
