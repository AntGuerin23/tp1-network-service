using tp1_network_service.Internal.Utils;

namespace tp1_network_service.Internal.Layers.FileListeners;

internal class SyncListeningStrategy : IListeningStrategy
{
    public void Listen(string filePath, Action<byte[]> callback, CancellationTokenSource cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var data = FileManager.Read(filePath);
            if (data.Length != 0)
            {
                callback(data);
            }
            Thread.Sleep(1000);
        } 
    }
}