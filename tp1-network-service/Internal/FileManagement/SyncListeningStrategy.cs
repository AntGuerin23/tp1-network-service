using System.Reflection;

namespace tp1_network_service.Internal.FileManagement;

internal class SyncListeningStrategy : IListeningStrategy
{
    public void Listen(string filePath, Action<byte[]> callback, CancellationTokenSource cancellationToken)
    {
        while (!cancellationToken.IsCancellationRequested)
        {
            var data = FileManager.ReadAndDeleteFirstLineOfFile(filePath);
            if (data.Length != 0)
            {
                var binaryString = string.Join("\n", data.Select(b => Convert.ToString(b, 2).PadLeft(8, '0')));
                callback(data);
            }
            Thread.Sleep(100);
        } 
    }
}