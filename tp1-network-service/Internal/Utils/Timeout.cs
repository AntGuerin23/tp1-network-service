namespace tp1_network_service.Internal;

internal class Timeout
{
    private readonly int _timeoutSeconds;
    private readonly AutoResetEvent _resetEvent;
    private readonly CancellationTokenSource _cancellationTokenSource;
    private bool _wasTimeoutCanceled;
    
    public Timeout(int timeoutSeconds)
    {
        _timeoutSeconds = timeoutSeconds;
        _cancellationTokenSource = new CancellationTokenSource();
        _resetEvent = new AutoResetEvent(false);
    }
    
    public bool WaitForTimeout()
    {
        _wasTimeoutCanceled = false;
        var timeoutTask = new Task(() => WaitTimeoutTime(_cancellationTokenSource.Token));
        timeoutTask.Start();
        _resetEvent.WaitOne();
        return _wasTimeoutCanceled;                         
    }

    public void CancelTimeout()
    {
        _wasTimeoutCanceled = true;
        _cancellationTokenSource.Cancel();
        _resetEvent.Set();
    }

    private void WaitTimeoutTime(CancellationToken token)
    {
        Thread.Sleep(_timeoutSeconds * 1000);
        if (token.IsCancellationRequested) return;
        _resetEvent.Set();
    }
}