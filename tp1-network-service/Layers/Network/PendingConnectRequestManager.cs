using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Layers.Network;

internal class PendingConnectRequestManager
{
    private ConnectPrimitive? _pendingConnectRequest;
    private readonly object _pendingConnectRequestLock = new();

    public void SetPendingConnect(ConnectPrimitive? connect)
    {
        lock (_pendingConnectRequestLock)
        {
            _pendingConnectRequest = connect;
        }
    }
}