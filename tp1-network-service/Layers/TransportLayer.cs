using System.Text;
using tp1_network_service.Messages;
using tp1_network_service.Messages.Builder;
using tp1_network_service.Serialization;

namespace tp1_network_service.Layers;

// TODO : Class should be internal with a Facade accessing it
public class TransportLayer : Layer
{
    private static TransportLayer? _instance;

    public TransportConnectionsHandler ConnectionsHandler { get; } = new();
    public FilePaths UpperLayerPaths { private get; set; }
    public FilePaths NetworkPaths { private get; set; }

    public static TransportLayer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new TransportLayer();
            }

            return _instance;
        }
    }

    public override void StartListening()
    {
        var upperLayerInputThread =
            new Thread(() => ListenInputFile(UpperLayerPaths.Input, InputThreadsCancelToken.Token));
        var networkInputThreads =
            new Thread(() => ListenInputFile(NetworkPaths.Input, InputThreadsCancelToken.Token));
        InputListenerThreads.AddRange([upperLayerInputThread, networkInputThreads]);
        upperLayerInputThread.Start();
        networkInputThreads.Start();
    }

    internal void SendMessageToNetworkLayer(Message message)
    {
        var fileManager = new FileManager(NetworkPaths.Output);
        fileManager.Write(MessageSerializer.Serialize(message));
    }

    protected override void HandleNewMessage(byte[] data, string fileName)
    {
        if (fileName.Equals(UpperLayerPaths.Input))
        {
            HandleRawMessageFromUpperLayer(data);
        }
        else if (fileName.Equals(NetworkPaths.Input))
        {
            HandleRawMessageFromNetwork(data);
        }
    }

    private void HandleRawMessageFromUpperLayer(byte[] data)
    {
        var connectMessage = InitNewConnection();
        ConnectionsHandler.StoreDataForConfirmedConnection(connectMessage.ConnectionNumber, data);
        SendMessageToNetworkLayer(connectMessage);
    }

    private void HandleRawMessageFromNetwork(byte[] data)
    {
        Console.WriteLine($"Received {Encoding.Default.GetString(data)} , From Network Layer");
        var message = MessageSerializer.Deserialize(data, true);
        message.Handle(true);
    }

    private ConnectMessage InitNewConnection()
    {
        var connectionId = ConnectionsHandler.CreateWaitingConnection();
        return CreateConnectMessage(connectionId);
    }

    private ConnectMessage CreateConnectMessage(int connectionId)
    {
        var messageBuilder = new MessageBuilder();
        return (ConnectMessage)messageBuilder.SetConnectionNumber((byte)connectionId)
            .SetSource(127)
            .SetDestination(128)
            .ToConnectMessage()
            .GetResult();
    }
}