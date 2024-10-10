namespace tp1_network_service.Layers;

public class TransportConnectionsHandler
{
    private enum ConnectionStatus
    {
        Waiting = 0,
        Confirmed = 1
    }
    
    private readonly Dictionary<int, ConnectionStatus> _connectionNumbers = new();
    private readonly object _connectionNumbersLock = new();
    private readonly Dictionary<int, byte[]> _pendingData = new();
    private readonly object _pendingDataLock = new();

    public void StoreDataForConfirmedConnection(int connectionNumber, byte[] data)
    {
        lock (_pendingDataLock)
        {
            _pendingData[connectionNumber] = data;
        } 
    }
    
    public void ConfirmWaitingConnection(int connectionNumber)
    {
        lock (_connectionNumbersLock)
        {
            if (_connectionNumbers.TryGetValue(connectionNumber, out var value) && value == ConnectionStatus.Waiting)
            {
                _connectionNumbers[connectionNumber] = ConnectionStatus.Confirmed;
            }
        }
    }

    public byte[] GetPendingData(int connectionNumber)
    {
        lock (_pendingDataLock)
        {
            if (!_pendingData.TryGetValue(connectionNumber, out var value) ||
                value.Length <= 0) return [];

            _pendingData.Remove(connectionNumber);

            return value;
        }
    }

    public void RemoveConnection(int connectionNumber)
    {
        lock (_connectionNumbersLock)
        {
            _connectionNumbers.Remove(connectionNumber);
        }
        lock (_pendingDataLock)
        {
            _pendingData.Remove(connectionNumber);
        }
    }

    public int CreateWaitingConnection()
    {
        var connectionNumber = CreateNewConnectionNumber();
        lock (_connectionNumbersLock)
        {
            _connectionNumbers[connectionNumber] = ConnectionStatus.Waiting;
        }
        return connectionNumber;
    }

    private int CreateNewConnectionNumber()
    {
        Random random = new();
        int connectionNumber;
        lock (_connectionNumbersLock)
        {
            do
            {
                connectionNumber = random.Next(0, 256);
            } while (_connectionNumbers.ContainsKey(connectionNumber));
        }
        return connectionNumber;
    }
}