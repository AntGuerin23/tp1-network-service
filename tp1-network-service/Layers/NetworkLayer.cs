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

    protected override void HandleNewMessage(byte[] data, string fileName)
    {
        if (fileName.Equals(dataLinkPaths.Input))
        {
            HandleRawMessageFromDataLink(data);
        } else if (fileName.Equals(transportPaths.Input))
        {
            HandleRawMessageFromTransport(data);
        }
        // Console.WriteLine($"({Environment.CurrentManagedThreadId}) Network layer received : : {Encoding.Default.GetString(data)}");
        // // TEST BELOW (Open terminal and echo "AAABBB" into L_LEC.txt to simulate a transfer between Network and Transport
        // // L_LEC -> NetworkLayer -> TransportLayer
        // if (Encoding.Default.GetString(data) == "AAABBB")
        // {
        //     var fileManager = new FileManager(transportPaths.Output);
        //     fileManager.Write(Encoding.UTF8.GetBytes("AAABBB RESPONSE"));
        // }
    }

    private void HandleRawMessageFromTransport(byte[] data)
    {
        Console.WriteLine($"Received {Encoding.Default.GetString(data)} , From Transport Layer");
    }

    private void HandleRawMessageFromDataLink(byte[] data)
    {
        Console.WriteLine($"Received {Encoding.Default.GetString(data)} , From DataLink Layer"); 
    }
}