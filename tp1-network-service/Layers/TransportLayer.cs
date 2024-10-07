using System.Text;
using tp1_network_service.Messages;
using tp1_network_service.Messages.Builder;
using tp1_network_service.Serialization;

namespace tp1_network_service.Layers;


public class TransportLayer(FilePaths upperLayerPaths, FilePaths networkPaths) : Layer
{
    private enum ConnectionStatus
    {
        Waiting = 0,
        Confirmed = 1
    }
    
    private readonly Dictionary<int, ConnectionStatus> _connectionNumbers = new();
    private readonly object _connectionNumbersLock = new();
    
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

    protected override void HandleNewMessage(byte[] data, string fileName)
    {
        if (fileName.Equals(upperLayerPaths.Input))
        {
            HandleRawMessageFromUpperLayer(data);
        } else if (fileName.Equals(networkPaths.Input))
        {
            HandleRawMessageFromNetwork(data);
        } 
    }

    private void HandleRawMessageFromUpperLayer(byte[] data)
    {
        var connectionId = CreateWaitingConnection();
        var messageBuilder = new MessageBuilder();
        var message = messageBuilder.SetConnectionNumber((byte)connectionId).SetSource(127).SetDestination(128)
            .ToConnectMessage().GetResult();
        var fileManager = new FileManager(networkPaths.Output);
        fileManager.Write(MessageSerializer.Serialize(message)); 
    }
    
    private void HandleRawMessageFromNetwork(byte[] data)
    {
        Console.WriteLine($"Received {Encoding.Default.GetString(data)} , From Network Layer");
    }

    private void ConfirmWaitingConnection(int connectionId)
    {
        lock (_connectionNumbersLock)
        {
            _connectionNumbers[connectionId] = ConnectionStatus.Confirmed;
        }
    }

    private int CreateWaitingConnection()
    {
        var connectionId = CreateNewConnectionId();
        lock (_connectionNumbersLock)
        {
            _connectionNumbers[connectionId] = ConnectionStatus.Waiting;
        }
        return connectionId;
    }

    private int CreateNewConnectionId()
    {
        Random random = new();
        int connectionId;
        lock (_connectionNumbersLock)
        {
            do
            {
                connectionId = random.Next(0, 256);
            } while (_connectionNumbers.ContainsKey(connectionId));
        }
        return connectionId;
    }
}