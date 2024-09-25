namespace System.Threading.Executions
{
	internal interface IExecutionWorker
	{
		void Start();

		void Execute();
	}
}
