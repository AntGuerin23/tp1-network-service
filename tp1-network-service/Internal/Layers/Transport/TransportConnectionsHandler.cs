using tp1_network_service.Internal.Enums;

namespace tp1_network_service.Internal.Layers.Transport;

internal class TransportConnectionsHandler
{
    private readonly Dictionary<int, TransportConnection> _connections = new();
    private readonly object _connectionsLock = new();

    public int CreateWaitingConnection(byte[] data)
    {
        Random random = new();
        int connectionNumber;
        lock (_connectionsLock)
        {
            do
            {
                connectionNumber = random.Next(0, 256);
            } while (_connections.ContainsKey(connectionNumber));
            
            _connections[connectionNumber] = new TransportConnection(TransportConnectionStatus.Waiting, data);
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
                _connections[connectionNumber].PendingData = [];
            }
        }
        return pendingData;
    }

    public void CloseConnection(int connectionNumber)
    {
        lock (_connectionsLock)
        {
            _connections.Remove(connectionNumber);
        }
    }
}