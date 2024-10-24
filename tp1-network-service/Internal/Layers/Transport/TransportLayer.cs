using tp1_network_service.External;
using tp1_network_service.External.Exceptions;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.FileManagement;
using tp1_network_service.Internal.Layers.Handling;
using tp1_network_service.Internal.Layers.Network;
using tp1_network_service.Internal.Primitives;
using tp1_network_service.Internal.Primitives.Abstract;

namespace tp1_network_service.Internal.Layers.Transport;

internal class TransportLayer : ILayer
{
    public TransportConnectionsHandler ConnectionsHandler { get; } = new();

    private static TransportLayer? _instance;
    private FilePaths? _upperLayerPaths;
    private readonly FileListener _fileListener = new(new SyncListeningStrategy());
    private readonly PrimitiveHandler _primitiveHandler = new(new TransportPrimitiveHandlerStrategy());
    
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
    
    public void SetPaths(FilePaths? paths)
    {
        _upperLayerPaths = paths;
    }

    public void Start()
    {
        if (_upperLayerPaths == null)
        {
            throw new FilePathNotSpecifiedException("Transport Layer : DataLink layer paths must be specified.");
        }
        _fileListener.Listen(_upperLayerPaths.Input, HandleFromFile);
    }

    public void Stop()
    {
        _fileListener.Stop();
    }

    public void HandleFromFile(byte[] data)
    {
        var connectionNumber = ConnectionsHandler.CreateWaitingConnection(data);
        var connectRequest = new PrimitiveBuilder()
            .SetConnectionNumber(connectionNumber)
            .SetSourceAddress(new Random().Next(1, 255))
            .SetDestinationAddress(new Random().Next(1, 255))
            .SetType(PrimitiveType.Req)
            .ToConnectPrimitive();
        NetworkLayer.Instance.HandleFromLayer(connectRequest);
    }

    public void HandleFromLayer(Primitive primitive)
    {
        _primitiveHandler.Handle(primitive);
    }
}