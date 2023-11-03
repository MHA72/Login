using System.Collections.Concurrent;
using Login.IRepositories;

namespace Login.Repositories;

public class BackgroundTaskQueue : IBackgroundTaskQueue
{
    private readonly SemaphoreSlim _signal = new(0);

    private readonly ConcurrentQueue<Func<CancellationToken, Task>> _workItems = new();

    public void QueueBackgroundWorkItem(Func<CancellationToken, Task> workItem)
    {
        if (workItem == null) throw new ArgumentNullException(nameof(workItem));
        _workItems.Enqueue(workItem);
        _signal.Release();
    }
}