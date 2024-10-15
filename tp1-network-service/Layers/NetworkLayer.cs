using System.Text;
using tp1_network_service.Builder;
using tp1_network_service.Interfaces;
using tp1_network_service.Packets;
using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Layers;

internal class NetworkLayer : Layer, INetworkLayer
{
    private static NetworkLayer? _instance;
    private (int, int)? _waitingConnectionNumberAndDestination;
    private readonly object _waitingConnectionNumberAndDestinationLock = new();
    public FilePaths DataLinkPaths { private get; set; }

    public static NetworkLayer Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new NetworkLayer();
            }

            return _instance;
        }
    }

    public override void StartListening()
    {
        var dataLinkInputThread =
            new Thread(() => ListenInputFile(DataLinkPaths.Input, InputThreadsCancelToken.Token));
        InputListenerThreads.Add(dataLinkInputThread);
        dataLinkInputThread.Start();
    }

    public (int, int)? GetAndResetWaitingConnectionNumberAndDestination()
    {
        lock (_waitingConnectionNumberAndDestinationLock)
        {
            var returnValue = _waitingConnectionNumberAndDestination;
            _waitingConnectionNumberAndDestination = null;
            return returnValue;
        }
    }

    public void SetWaitingConnectionNumberAndDestination(int connectionNumber, int destination)
    {
        lock (_waitingConnectionNumberAndDestinationLock)
        {
            _waitingConnectionNumberAndDestination = (connectionNumber, destination);
        }
    }

    public void SendMessageToDataLinkLayer(Primitive primitive)
    {
      //  FileManager.Write(PacketSerializer.Serialize(primitive), DataLinkPaths.Output);
    }

    protected override void HandleNewMessage(byte[] data, string fileName)
    {
        //var message = PacketSerializer.Deserialize(data, false);
        //message.Handle();
    }
    private bool IsNetworkServiceError(int source) => (source & 27) == 0;

    public void Connect(ConnectPrimitive connectPrimitive)
    {
        if (IsNetworkServiceError(connectPrimitive.SourceAddress))
        {
            var disconnectMessage = new PrimitiveBuilder().SetConnectionNumber(connectPrimitive.ConnectionNumber)
                .SetSourceAddress(connectPrimitive.DestinationAddress)
                .SetDestinationAddress(connectPrimitive.SourceAddress)
                .SetReason(DisconnectReason.NetworkService)
                .ToDisconnectPrimitive();
            TransportLayer.Instance.Disconnect(disconnectMessage);
            return;
        }
        SetWaitingConnectionNumberAndDestination(connectPrimitive.ConnectionNumber, connectPrimitive.DestinationAddress);
        var connectIndMessage = new PrimitiveBuilder().SetConnectionNumber(connectPrimitive.ConnectionNumber)
            .SetSourceAddress(connectPrimitive.SourceAddress)
            .SetDestinationAddress(connectPrimitive.DestinationAddress)
            .SetType(PrimitiveType.Ind)
            .ToConnectPrimitive();
        Instance.SendMessageToDataLinkLayer(connectIndMessage);
    }

    public void Disconnect(DisconnectPrimitive disconnectPrimitive)
    {
        Instance.SendMessageToDataLinkLayer(disconnectPrimitive);
    }

    public void SendData(DataPrimitive dataPrimitive)
    {
        Instance.SendMessageToDataLinkLayer(dataPrimitive);
    }

    private void HandleConnectResponse(ConnectPrimitive connectPrimitive)
    {
        // var messageBuilder = new MessageBuilder();
        //     var waitingConnection = NetworkLayer.Instance.GetAndResetWaitingConnectionNumberAndDestination();
        //     if (waitingConnection != null && waitingConnection.Value.Item1 != ConnectionNumber)
        //     {
        //         var disconnectMessage = messageBuilder.SetConnectionNumber((byte)waitingConnection.Value.Item1)
        //             .SetSource(waitingConnection.Value.Item2)
        //             .SetPrimitive(MessagePrimitive.Ind)
        //             .ToDisconnectMessage(DisconnectReason.Self)
        //             .GetResult();
        //         NetworkLayer.Instance.SendMessageToTransportLayer(disconnectMessage);
        //     }
        //     var connectResponseMessage = messageBuilder.SetConnectionNumber((byte)ConnectionNumber)
        //         .SetSource(Source)
        //         .SetDestination(Destination)
        //         .SetPrimitive(MessagePrimitive.Conf)
        //         .ToConnectMessage()
        //         .GetResult();
        //     NetworkLayer.Instance.SendMessageToTransportLayer(connectResponseMessage);
    }
}