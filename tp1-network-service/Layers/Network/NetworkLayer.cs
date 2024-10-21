using tp1_network_service.Exceptions;
using tp1_network_service.Layers.FileListeners;
using tp1_network_service.Packets.Abstract;
using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Utils;
using tp1_network_service.Visitors;

namespace tp1_network_service.Layers.Network;

public class NetworkLayer : ILayer, IPacketVisitor
{
    public PendingConnectRequestManager PendingConnectRequestManager { get; } = new();
    
    private static NetworkLayer? _instance;
    private FilePaths? _dataLinkPaths;
    private readonly FileListener _fileListener = new FileListener(new AsyncListeningStrategy());
    private readonly PrimitiveHandler _primitiveHandler = new PrimitiveHandler(new NetworkPrimitiveHandlerStrategy());
    private readonly PacketHandler _packetHandler = new PacketHandler();
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
        _packetHandler.Handle(packet);
    }

    public void HandleFromLayer(Primitive primitive)
    {
        _primitiveHandler.Handle(primitive);
    }

    public void SendPrimitiveToDataLinkLayer(Primitive primitive)
    {
        primitive.Accept(this);
    }

    public void Visit(Packet packet)
    {
        FileManager.Write(packet.Serialize(), _dataLinkPaths.Output);
    }

    public void Visit(IEnumerable<Packet> packets)
    {
        throw new NotImplementedException();
    }
}