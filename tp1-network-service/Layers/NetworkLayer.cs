using System.Text;
using tp1_network_service.Messages;

namespace tp1_network_service.Layers;

public class NetworkLayer (FilePaths dataLinkPaths, FilePaths transportPaths) : Layer 
{
    public override void StartListening()
    {
        var dataLinkInputThread = 
            new Thread(() => ListenInputFile(dataLinkPaths.Input, InputThreadsCancelToken.Token));
        var transportInputThread =
            new Thread(() => ListenInputFile(transportPaths.Input, InputThreadsCancelToken.Token));
        InputListenerThreads.AddRange([dataLinkInputThread, transportInputThread]);
        dataLinkInputThread.Start();
        transportInputThread.Start();
    }

    internal override void HandleNewMessage(Message message)
    {
        throw new NotImplementedException();
    }

    protected override void HandleRawMessage(byte[] data)
    {
        Console.WriteLine($"({Environment.CurrentManagedThreadId}) Network layer received : : {Encoding.Default.GetString(data)}");
        // TEST BELOW (Open terminal and echo "AAABBB" into L_LEC.txt to simulate a transfer between Network and Transport
        // L_LEC -> NetworkLayer -> TransportLayer
        if (Encoding.Default.GetString(data) == "AAABBB")
        {
            var fileManager = new FileManager(transportPaths.Output);
            fileManager.Write(Encoding.UTF8.GetBytes("AAABBB RESPONSE"));
        }
    }
}