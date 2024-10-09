using System.Text;
using tp1_network_service.Enums;
using tp1_network_service.Messages;
using tp1_network_service.Messages.Builder;
using tp1_network_service.Serialization;

namespace tp1_network_service.Layers;


public class TransportLayer(FilePaths upperLayerPaths, FilePaths networkPaths) : Layer
{
    private readonly TransportConnectionsHandler _connectionsHandler = new();

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
        var connectMessage = InitNewConnection();
        _connectionsHandler.StoreDataForConfirmedConnection(connectMessage.ConnectionNumber, data);
        SendMessageToDataLinkLayer(connectMessage);
    }
    
    private void HandleRawMessageFromNetwork(byte[] data)
    {
        Console.WriteLine($"Received {Encoding.Default.GetString(data)} , From Network Layer");
        var message = MessageSerializer.Deserialize(data, true);
        if (message is ConnectMessage connectMessage)
        {
            HandleConnectMessage(connectMessage);
        }
    }

    private ConnectMessage InitNewConnection()
    {
        var connectionId = _connectionsHandler.CreateWaitingConnection();
        return CreateConnectMessage(connectionId);
    }

    private void SendMessageToDataLinkLayer(Message message)
    {
        var fileManager = new FileManager(networkPaths.Output);
        fileManager.Write(MessageSerializer.Serialize(message)); 
    }

    private ConnectMessage CreateConnectMessage(int connectionId)
    {
        var messageBuilder = new MessageBuilder();
        return (ConnectMessage) messageBuilder.SetConnectionNumber((byte)connectionId)
                                              .SetSource(127)
                                              .SetDestination(128)
                                              .ToConnectMessage()
                                              .GetResult();
    }
    
    private void HandleConnectMessage(ConnectMessage message)
    {
        switch (message.Primitive)
        {
            case MessagePrimitive.Conf:
                HandleConnectConfirmationMessage(message);
                break;
            case MessagePrimitive.Req:
                break;
            case MessagePrimitive.Ind:
                break;
            case MessagePrimitive.Resp:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void HandleConnectConfirmationMessage(ConnectMessage message)
    {
        _connectionsHandler.ConfirmWaitingConnection(message.ConnectionNumber);
        var pendingData = _connectionsHandler.GetPendingData(message.ConnectionNumber);
        
        if (pendingData.Length <= 0) return;
        
        var messageBuilder = new MessageBuilder();
        // TODO : Change bottom section for segmentation
        var segInfo = new SegmentationInfo(2);
        var dataMessage = messageBuilder.SetConnectionNumber((byte) message.ConnectionNumber)
            .SetSource((byte) message.Source) //TODO : Null check
            .SetDestination((byte) message.Destination) //TODO : Null check
            .ToDataMessage(segInfo, pendingData)
            .GetResult();
        SendMessageToDataLinkLayer(dataMessage);
    }
}