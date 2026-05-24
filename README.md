# System.Threading namespace Extensions

## System.Threading.AsyncLock

* Task LockAsync(Func<CacellationToken, Task> func, CancellationToken cancellationToken)

## System.Threading.ICountdown interface

* void AddCount(int signalCount)
* bool TryAddCount(int signalCount)
* bool TryAddCount()
* bool Signal(int signalCount)
* bool Signal()
* void Reset(int count)
* void Reset()
* void Wait()
* void Wait(CancellationToken cancellationToken)
* void Wait(TimeSpan timeout)

### System.Threading.Countdown

## System.Threading.CountdownFactory

* ICountdown Create(int initialCount)

## System.Threading.Atomic namesapce

### System.Threading.Atomic.IAtomic interface

* AtomicBool
* AtomicInt32
* AtomicInt64

## System.Threading.Executions

* System.Threading.Executions.ExecutionType enum (Async, Sync)

### System.Threading.Executions.IExecutor interface

#### DefaultExecutor

* void Execute(IExecution execution, TimeSpan interval)
* void Execute(IExecution execution, CronExpression expression)
* void Shutdown()

### System.Threading.Executions.IExecution interface

#### System.Threading.Executions.AsyncExecution abstract calss

* Task ExecuteAsync(CancellationToken cancellationToken)

#### System.Threading.Executions.SyncExecution abstract class

* void Execute(CancellationToken cancellationToken)

## System.Threading.PerformanceCounter namespace

### System.Threading.PerformanceCounter.IPerformaceLogListener interface

* void Listen(IDictionary<string, PerformanceMetric> dic)

#### DefaultPerformaceLogListener

### System.Threading.PerformaceCounter.PerformanceCounterProvider

* AtomicInt64 GetCounter(string id)

### System.Threading.PerformaceCounter.PerformanceLogger

* void Initialize(HashSet<string> idSet, TimeSpan interval)

## System.Threading.Tasks namespace Extensions

* TaskType enum (Async, Sync)

### System.Threading.Tasks.ITaskSchedualer interface

* void AddTask(string id, ITask task, TimeSpan interval)
* void AddTask(string id, ITask task, CronExpression expresion)
* bool ContainTask(string id)
* DateTime? NextExecutionTime(string id)
* void WakeUp(string id)
* void RemoveTask(string id)
* void WaitForShutdown()


#### System.Threading.Tasks.DefaultTaskScheduler

### System.Threading.Tasks.ITask interface

#### System.Threading.Tasks.AsyncTask abstract class

* Task RunAsync(CancellationToken cancellationToken)

#### System.Threading.Tasks.SyncTask abstract class

* void Run(CancellationToken cancellationToken)