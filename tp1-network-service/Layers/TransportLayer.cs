using tp1_network_service.Builder;
using tp1_network_service.Interfaces;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Layers;

internal class TransportLayer : Layer, ITransportLayer
{
    private static TransportLayer? _instance;
    private TransportConnectionsHandler ConnectionsHandler { get; } = new();
    public FilePaths UpperLayerPaths { private get; set; }

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
        var upperLayerInputThread = new Thread(() => ListenInputFile(UpperLayerPaths.Input, InputThreadsCancelToken.Token));
        InputListenerThreads.Add(upperLayerInputThread);
        upperLayerInputThread.Start();
    }
    
    public void ConfirmConnection(ConnectPrimitive connectPrimitive)
    {
        ConnectionsHandler.ConfirmWaitingConnection(connectPrimitive.ConnectionNumber);
        var pendingData = ConnectionsHandler.GetPendingData(connectPrimitive.ConnectionNumber);
        
        if (pendingData.Length <= 0) return;

        var dataPrimitive = new PrimitiveBuilder().SetConnectionNumber(connectPrimitive.ConnectionNumber)
            .SetSourceAddress(connectPrimitive.DestinationAddress)
            .SetDestinationAddress(connectPrimitive.SourceAddress)
            .SetData(pendingData)
            .ToDataPrimitive();
        NetworkLayer.Instance.SendData(dataPrimitive);
    }

    public void Disconnect(DisconnectPrimitive disconnectPrimitive)
    {
        ConnectionsHandler.RemoveConnection(disconnectPrimitive.ConnectionNumber);
    }

    protected override void HandleNewMessage(byte[] data, string fileName)
    {
        var connectMessage = InitNewConnection();
        ConnectionsHandler.StoreDataForConfirmedConnection(connectMessage.ConnectionNumber, data);
        NetworkLayer.Instance.Connect(connectMessage);
    }

    private ConnectPrimitive InitNewConnection()
    {
        var connectionId = ConnectionsHandler.CreateWaitingConnection();
        return CreateConnectMessage(connectionId);
    }

    private ConnectPrimitive CreateConnectMessage(int connectionId)
    {
        return new PrimitiveBuilder().SetConnectionNumber(connectionId)
            .SetSourceAddress(127)
            .SetDestinationAddress(128)
            .ToConnectPrimitive();
    }
}