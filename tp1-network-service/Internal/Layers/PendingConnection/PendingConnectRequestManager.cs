using System.Collections.Concurrent;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.Layers.Transport;
using tp1_network_service.Internal.Primitives;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.PendingConnection;

internal class PendingConnectRequestManager
{
    private const int ConnectResponseTimeoutSeconds = 5;

    private ConcurrentDictionary<int, Timeout> _pendingConnections = new();

    public void WaitForResponse(int connectionNumber)
    {
        var timeout = new Timeout(ConnectResponseTimeoutSeconds);
        _pendingConnections.TryAdd(connectionNumber, timeout);

        var success = timeout.WaitForTimeout();
        if (!success)
        {
            Disconnect(connectionNumber);
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

    private void Disconnect(int connectionNumber)
    {
        TransportLayer.Instance.HandleFromLayer(new PrimitiveBuilder()
            .SetConnectionNumber(connectionNumber)
            .SetType(PrimitiveType.Ind)
            .SetReason(DisconnectReason.Distant)
            .ToDisconnectPrimitive());
    }
}