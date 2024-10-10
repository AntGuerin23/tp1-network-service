using tp1_network_service.Messages;

namespace tp1_network_service.Layers;

public abstract class Layer
{
    protected readonly CancellationTokenSource InputThreadsCancelToken = new();
    protected readonly List<Thread> InputListenerThreads = [];
    public abstract void StartListening();
    protected abstract void HandleNewMessage(byte[] data, string fileName);

    public void StopListening()
    {
        Console.WriteLine("Stopping file listeners threads");
        InputThreadsCancelToken.Cancel();
        foreach (var thread in InputListenerThreads)
        {
            if (thread.IsAlive)
            {
                thread.Join();
            }
        }
    }
    
    protected void ListenInputFile(string filePath, CancellationToken cancellationToken)
    {
        Console.WriteLine($"Started listening on thread {Environment.CurrentManagedThreadId}");
        while (!cancellationToken.IsCancellationRequested)
        {
            var data = FileManager.Read(filePath);
            if (data.Length != 0)
            {
                new Thread(() => HandleNewMessage(data, filePath)).Start(); //TODO : Seulement network devrait être asynchrone 
            }
            Thread.Sleep(1000);
        }
    }
}