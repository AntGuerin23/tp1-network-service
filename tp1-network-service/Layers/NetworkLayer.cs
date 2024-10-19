using tp1_network_service.Builder;
using tp1_network_service.Enums;
using tp1_network_service.Interfaces;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Abstract;
using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Children;
using tp1_network_service.Utils;

namespace tp1_network_service.Layers;

internal class NetworkLayer : Layer, INetworkLayer
{
    private static NetworkLayer? _instance;
    private (int, int)? _waitingConnectionNumberAndDestination;
    private readonly object _waitingConnectionNumberAndDestinationLock = new();
    private PacketSegmenter? _currentPacketSegmenter; 
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
        InputListenerThread = new Thread(() => ListenInputFile(DataLinkPaths.Input, InputThreadsCancelToken.Token));
        InputListenerThread.Start();
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

    public void SendPacketToDataLinkLayer(Packet packet)
    {
        FileManager.Write(packet.Serialize(), DataLinkPaths.Output);
    }

    protected override void HandleInput(byte[] data)
    {
        var packet = Deserializer.DeserializePacket(data);
        packet.Handle();
        //var message = PacketSerializer.Deserialize(data, false);
        //message.Handle();
    }

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
        Instance.SendPacketToDataLinkLayer(connectIndMessage.GeneratePacket());
    }

    public void Disconnect(DisconnectPrimitive disconnectPrimitive)
    {
        Instance.SendPacketToDataLinkLayer(disconnectPrimitive.GeneratePacket());
    }

    public void SendData(DataPrimitive dataPrimitive)
    {
        var segmenter = new PacketSegmenter(dataPrimitive);
        _currentPacketSegmenter = segmenter;
        Instance.SendPacketToDataLinkLayer(segmenter.ConstructNextPacket());
        WaitForTimeout();
    }

    private void WaitForTimeout()
    {
        _currentPacketSegmenter?.WaitForAcknowledgementTimeout();

        if (_currentPacketSegmenter != null && !_currentPacketSegmenter.LastSegmentWasAcknowledged())
        {
            //TODO : Send disconnect
        }
    }

    
    private bool IsNetworkServiceError(int source) => source % 27 == 0;

    private NetworkLayer() { }
}