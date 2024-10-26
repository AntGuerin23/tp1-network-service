using tp1_network_service.Internal.FileManagement.FileManagers;

namespace tp1_network_service.Internal.FileManagement;

internal class AsyncListeningStrategy : IListeningStrategy
{
    private readonly IFileManager _fileManager;
    
    public AsyncListeningStrategy(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }
    
    public void Listen(string filePath, Action<byte[]> callback, CancellationTokenSource cancellationToken)
    {
        ThreadPool.SetMinThreads(50, 50);
        while (!cancellationToken.IsCancellationRequested)
        {
            var data = _fileManager.ReadAndDeleteFirstLine(filePath);
            if (data.Length != 0)
            {
                Task.Run(() => callback(data));
            }
            Thread.Sleep(10);
        } 
    }
}