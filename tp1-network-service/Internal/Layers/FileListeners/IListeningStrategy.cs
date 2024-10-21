namespace tp1_network_service.Internal.Layers.FileListeners;

internal interface IListeningStrategy
{
    public void Listen(string filePath, Action<byte[]> callback, CancellationTokenSource cancellationToken);
}