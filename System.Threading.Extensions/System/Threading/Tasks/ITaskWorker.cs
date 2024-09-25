namespace System.Threading.Tasks
{
	internal interface ITaskWorker
	{
		DateTime? NextExecutionTime { get; }

		void Start();

		void WakeUp();

		void Run();

		void Join();

		void Stop();
	}
}
