using cms_bd.Data;

namespace cms_bd;

public abstract class BackgroundService : IHostedService
{
    private Task _executingTask;
    private readonly CancellationTokenSource _stoppingCts = new();

    public virtual Task StartAsync(CancellationToken cancellationToken)
    {
        // Store the task we're executing
        _executingTask = ExecuteAsync(_stoppingCts.Token);

        // If the task is completed then return it,
        // this will bubble cancellation and failure to the caller
        // Otherwise it's running
        return _executingTask.IsCompleted ? _executingTask : Task.CompletedTask;
    }

    public virtual async Task StopAsync(CancellationToken cancellationToken)
    {
        // Stop called without start
        if (_executingTask == null)
        {
            return;
        }

        try
        {
            // Signal cancellation to the executing method
            _stoppingCts.Cancel();
        }
        finally
        {
            // Wait until the task completes or the stop token triggers
            await Task.WhenAny(_executingTask, Task.Delay(Timeout.Infinite,
                cancellationToken));
        }
    }

    protected virtual async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //stoppingToken.Register(() =>
        //        _logger.LogDebug($" GracePeriod background task is stopping."));

        do
        {
            await Process();
            await Task.Delay(5000, stoppingToken); //5 second delay
        }
        while (!stoppingToken.IsCancellationRequested);
    }

    protected abstract Task Process();
}