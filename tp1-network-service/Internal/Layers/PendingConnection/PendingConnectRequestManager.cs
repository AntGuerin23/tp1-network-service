using System.Collections.Concurrent;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.Layers.Transport;
using tp1_network_service.Internal.Primitives;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.PendingConnection;

internal class PendingConnectRequestManager
{
    private const int ConnectResponseTimeoutSeconds = 1;

    private ConcurrentDictionary<int, Timeout> _pendingConnections = new();

    public void WaitForResponse(ConnectPrimitive primitive)
    {
        var timeout = new Timeout(ConnectResponseTimeoutSeconds);
        _pendingConnections.TryAdd(primitive.ConnectionNumber, timeout);

        var success = timeout.WaitForTimeout();
        if (!success)
        {
            Disconnect(primitive);
        }
    }

    public bool ConfirmConnection(int connectionNumber)
    {
        var success = _pendingConnections.TryGetValue(connectionNumber, out Timeout? timeout);
        if (success)
        {
            timeout!.CancelTimeout();
            _pendingConnections.Remove(connectionNumber,out _);
        }
        return success;
    }

    private void Disconnect(ConnectPrimitive primitive)
    {
        TransportLayer.Instance.HandleFromLayer(new PrimitiveBuilder()
            .SetConnectionNumber(primitive.ConnectionNumber)
            .SetSourceAddress(primitive.SourceAddress)
            .SetDestinationAddress(primitive.DestinationAddress)
            .SetType(PrimitiveType.Ind)
            .SetReason(DisconnectReason.Distant)
            .ToDisconnectPrimitive());
    }
}