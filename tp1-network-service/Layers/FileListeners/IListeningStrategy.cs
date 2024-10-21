namespace tp1_network_service.Layers.FileListeners;

public interface IListeningStrategy
{
    public void Listen(string filePath, Action<byte[]> callback, CancellationTokenSource cancellationToken);
}