using tp1_network_service.Builder;
using tp1_network_service.Enums;
using tp1_network_service.Exceptions;
using tp1_network_service.Interfaces;
using tp1_network_service.Layers.FileListeners;
using tp1_network_service.Layers.Transport;
using tp1_network_service.Packets.Abstract;
using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Primitives.Children;
using tp1_network_service.Utils;

namespace tp1_network_service.Layers.Network;

internal class NetworkLayer : ILayer, INetworkLayer
{
    public PendingConnectRequestManager PendingConnectRequestManager { get; } = new();
    public PendingSendingDataManager PendingSendingDataManager { get; } = new();
    
    private static NetworkLayer? _instance;
    private readonly FileListener _fileListener = new(new AsyncListeningStrategy());
    private FilePaths? _dataLinkPaths;

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

    public void SendPacketToDataLinkLayer(Packet packet)
    {
        FileManager.Write(packet.Serialize(), _dataLinkPaths.Output);
    }

    public void Connect(ConnectPrimitive connectPrimitive)
    {
        if (connectPrimitive.Type == PrimitiveType.Req) return;
        PendingConnectRequestManager.SetPendingConnect(connectPrimitive);
        SendPacketToDataLinkLayer(connectPrimitive.GeneratePacket());
    }

    public void Disconnect(DisconnectPrimitive disconnectPrimitive)
    {
        Instance.SendPacketToDataLinkLayer(disconnectPrimitive.GeneratePacket());
    }

    public void SendData(DataPrimitive dataPrimitive)
    {
        var success = PendingSendingDataManager.StartSendingData(dataPrimitive);
        if (!success)
        {
            TransportLayer.Instance.HandleFromLayer(new PrimitiveBuilder()
                .SetConnectionNumber(dataPrimitive.ConnectionNumber)
                .SetType(PrimitiveType.Ind)
                .SetReason(DisconnectReason.Distant)
                .ToDisconnectPrimitive());
        }
    }
    
    private NetworkLayer() { }
    
    public void SetPaths(FilePaths? paths)
    {
        _dataLinkPaths = paths;
    }

    public void Start()
    {
        if (_dataLinkPaths == null)
        {
            throw new FilePathNotSpecifiedException("Network Layer : DataLink layer paths must be specified.");
        }
        _fileListener.Listen(_dataLinkPaths.Input, HandleFromFile);
    }

    public void Stop()
    {
        _fileListener.Stop();
    }

    public void HandleFromFile(byte[] data)
    {
        var packet = PacketDeserializer.Deserialize(data);
        packet.Handle();
    }

    public void HandleFromLayer(Primitive primitive)
    {
        //todo
        //_primitiveHandler.Handle(primitive);
    }
    
    private bool IsNetworkServiceError(int source) => source % 27 == 0;
}