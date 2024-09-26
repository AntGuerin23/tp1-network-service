using tp1_network_service.Messages;

namespace tp1_network_service.Layers;

public class TransportLayer(FilePaths upperLayerPaths, FilePaths networkPaths) : Layer
{
    public override void StartListening()
    {
        var upperLayerInputThread =
            new Thread(() => ListenInputFile(upperLayerPaths.Input, InputThreadsCancelToken.Token));
        var networkInputThreads =
            new Thread(() => ListenInputFile(networkPaths.Input, InputThreadsCancelToken.Token));
        InputListenerThreads.AddRange([upperLayerInputThread, networkInputThreads]);
        upperLayerInputThread.Start();
        networkInputThreads.Start();
    }

    internal override void HandleNewMessage(Message message)
    {
        throw new NotImplementedException();
    }

    protected override void HandleRawMessage(byte[] data)
    {
        Console.WriteLine(
            $"({Environment.CurrentManagedThreadId}) Transport layer received : : {System.Text.Encoding.Default.GetString(data)}");
    }
}