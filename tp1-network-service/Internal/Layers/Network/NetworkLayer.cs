using System.Xml;
using tp1_network_service.External;
using tp1_network_service.External.Exceptions;
using tp1_network_service.Internal.FileManagement;
using tp1_network_service.Internal.Layers.Handling;
using tp1_network_service.Internal.Layers.Network.DataSending;
using tp1_network_service.Internal.Layers.PendingConnection;
using tp1_network_service.Internal.Packets;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Primitives.Abstract;

namespace tp1_network_service.Internal.Layers.Network;

internal class NetworkLayer : ILayer
{
    public DataSendingManager DataSendingManager { get; } = new();
    public PendingConnectRequestManager PendingConnectionRequestManager { get; } = new();
    
    private FilePaths? _dataLinkPaths;
    private static NetworkLayer? _instance;
    private readonly FileListener _fileListener = new(new AsyncListeningStrategy());
    private readonly PrimitiveHandler _primitiveHandler = new(new NetworkPrimitiveHandlerStrategy());

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
        _primitiveHandler.Handle(primitive);
    }
    
    public void SendPacket(Packet packet)
    {
        FileManager.WriteWithNewLine(packet.Serialize(), _dataLinkPaths!.Output);
    }
    
    private NetworkLayer() { }
}