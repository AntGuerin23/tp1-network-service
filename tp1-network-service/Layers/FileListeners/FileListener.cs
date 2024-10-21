namespace tp1_network_service.Layers.FileListeners;

public class FileListener(IListeningStrategy listeningStrategy)
{
    private Thread _thread;
    private readonly CancellationTokenSource _cancellationToken = new();
    
    public void Listen(string filePath, Action<byte[]> callback)
    {
        _thread = new Thread(() =>
        {
            listeningStrategy.Listen(filePath, callback, _cancellationToken);
        });
        _thread.Start();
    }

    public void Stop()
    {
        _cancellationToken.Cancel();
        if (_thread.IsAlive)
        {
            _thread.Join();
        }
    }
}