using System.Reflection;
using tp1_network_service.Internal.FileManagement.FileManagers;

namespace tp1_network_service.Internal.FileManagement;

internal class SyncListeningStrategy : IListeningStrategy
{
    private readonly IFileManager _fileManager;
    
    public SyncListeningStrategy(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }
    
    public void Listen(string filePath, Action<byte[]> callback, CancellationTokenSource cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var data = _fileManager.ReadAndDeleteFirstLine(filePath);
            if (data.Length != 0)
            {
                callback(data);
            }
            Thread.Sleep(10);
        } 
    }
}