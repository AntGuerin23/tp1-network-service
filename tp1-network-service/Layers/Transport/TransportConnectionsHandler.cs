using tp1_network_service.Enums;

namespace tp1_network_service.Layers.Transport;

internal class TransportConnectionsHandler
{
    private readonly Dictionary<int, TransportConnection> _connections = new();
    private readonly object _connectionsLock = new();

    public int CreateWaitingConnection()
    {
        Random random = new();
        int connectionNumber;
        lock (_connectionsLock)
        {
            do
            {
                connectionNumber = random.Next(0, 256);
            } while (_connections.ContainsKey(connectionNumber));
            
            _connections[connectionNumber] = new TransportConnection(TransportConnectionStatus.Waiting);
        }
        return connectionNumber;
    }

    public byte[] ConfirmWaitingConnectionAndGetPendingData(int connectionNumber)
    {
        var pendingData = Array.Empty<byte>();
        lock (_connectionsLock)
        {
            if (_connections.TryGetValue(connectionNumber, out var value) && value.Status == TransportConnectionStatus.Waiting)
            {
                _connections[connectionNumber].Status = TransportConnectionStatus.Confirmed;
                pendingData = _connections[connectionNumber].PendingData;
            }
        }
        return pendingData;
    }
}