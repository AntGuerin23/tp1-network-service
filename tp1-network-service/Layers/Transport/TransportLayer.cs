using tp1_network_service.Builder;
using tp1_network_service.Exceptions;
using tp1_network_service.Layers.FileListeners;
using tp1_network_service.Layers.Network;
using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Utils;

namespace tp1_network_service.Layers.Transport;

internal class TransportLayer : ILayer
{
    public TransportConnectionsHandler ConnectionsHandler { get; } = new();

    private static TransportLayer? _instance;
    private FilePaths? _upperLayerPaths;
    private readonly FileListener _fileListener = new FileListener(new SyncListeningStrategy());
    
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
        var connectionNumber = ConnectionsHandler.CreateWaitingConnection();
        var connectRequest = new PrimitiveBuilder()
            .SetConnectionNumber(connectionNumber)
            .SetSourceAddress(127) // TODO : Generate random 1-255
            .SetDestinationAddress(128) // TODO : Generate random 1-255
            .SetType(PrimitiveType.Req)
            .ToConnectPrimitive();
        NetworkLayer.Instance.HandleFromLayer(connectRequest);
    }

    public void HandleFromLayer(Primitive primitive)
    {
        //todo 
    }
}