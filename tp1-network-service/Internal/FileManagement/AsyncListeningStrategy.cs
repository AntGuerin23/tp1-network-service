namespace tp1_network_service.Internal.FileManagement;

internal class AsyncListeningStrategy : IListeningStrategy
{
    public void Listen(string filePath, Action<byte[]> callback, CancellationTokenSource cancellationToken)
    {
        ThreadPool.SetMinThreads(50, 50);
        while (!cancellationToken.IsCancellationRequested)
        {
            var data = FileManager.ReadAndDeleteFirstLineOfFile(filePath);
            if (data.Length != 0)
            {
                Task.Run(() => callback(data));
            }
            Thread.Sleep(100);
        } 
    }
}