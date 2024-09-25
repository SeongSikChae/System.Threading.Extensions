using Quartz;

namespace System.Threading.Executions
{
    /// <summary>
    /// IExecution 실행기 인터페이스
    /// </summary>
	public interface IExecutor
    {
        /// <summary>
        /// TimeSpan Interval 실행 요청
        /// </summary>
        /// <param name="execution"></param>
        /// <param name="interval"></param>
        void Execute(IExecution execution, TimeSpan interval);

        /// <summary>
        /// Cron Expression 실행 요청
        /// </summary>
        /// <param name="execution"></param>
        /// <param name="expression"></param>
        void Execute(IExecution execution, CronExpression expression);

        /// <summary>
        /// 모든 실행 요청이 중지
        /// </summary>
        void Shutdown();
    }
}
