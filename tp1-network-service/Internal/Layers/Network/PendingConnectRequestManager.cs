using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Network;

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

    public ConnectPrimitive? GetAndResetConnection()
    {
        ConnectPrimitive? connect;
        lock (_pendingConnectRequestLock)
        {
            connect = _pendingConnectRequest;
            _pendingConnectRequest = null;
        }
        return connect;
    }
}