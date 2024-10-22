namespace tp1_network_service.Internal.FileManagement;

internal class AsyncListeningStrategy : IListeningStrategy
{
    public void Listen(string filePath, Action<byte[]> callback, CancellationTokenSource cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var data = FileManager.Read(filePath);
            if (data.Length != 0)
            {
                new Thread(() => callback(data)).Start();
            }
            Thread.Sleep(1000);
        } 
    }
}