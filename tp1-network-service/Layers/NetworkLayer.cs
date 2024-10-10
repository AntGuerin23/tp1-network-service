using System.Text;
using tp1_network_service.Enums;
using tp1_network_service.Messages;
using tp1_network_service.Serialization;

namespace tp1_network_service.Layers;

public class NetworkLayer : Layer
{
    private static NetworkLayer? _instance;
    private (int, byte)? _waitingConnectionNumberAndDestination;
    private object _waitingConnectionNumberAndDestinationLock = new();
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
        // var dataLinkInputThread =
        //     new Thread(() => ListenInputFile(DataLinkPaths.Input, InputThreadsCancelToken.Token));
        // var transportInputThread =
        //     new Thread(() => ListenInputFile(TransportPaths.Input, InputThreadsCancelToken.Token));
        // InputListenerThreads.AddRange([dataLinkInputThread, transportInputThread]);
        // dataLinkInputThread.Start();
        // transportInputThread.Start();
    }

    internal (int, byte)? GetAndResetWaitingConnectionNumberAndDestination()
    {
        lock (_waitingConnectionNumberAndDestinationLock)
        {
            var returnValue = _waitingConnectionNumberAndDestination;
            _waitingConnectionNumberAndDestination = null;
            return returnValue;
        }
    }

    internal void SetWaitingConnectionNumberAndDestination(int connectionNumber, byte destination)
    {
        lock (_waitingConnectionNumberAndDestinationLock)
        {
            _waitingConnectionNumberAndDestination = (connectionNumber, destination);
        }
    }

    internal void SendMessageToDataLinkLayer(Primitive primitive)
    {
        FileManager.Write(MessageSerializer.Serialize(primitive), DataLinkPaths.Output);
    }

    protected override void HandleNewMessage(byte[] data, string fileName)
    {
        var message = MessageSerializer.Deserialize(data, false);
        message.Handle();
    }
}