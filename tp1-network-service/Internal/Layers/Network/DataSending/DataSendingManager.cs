using System.Collections.Concurrent;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Network.DataSending;

internal class DataSendingManager
{
    private readonly ConcurrentDictionary<int, DataSender> _dataSenders = new();
    private readonly object _dataSendersLock = new();

    public bool StartSendingData(DataPrimitive primitive)
    {
        var sender = new DataSender(primitive);
        _dataSenders.TryAdd(primitive.ConnectionNumber, sender);
        return sender.SendData();
    }
    
    public void CancelAcknowledgementWait(int connectionNumber)
    {
        var found = _dataSenders.TryGetValue(connectionNumber, out var sender);
        if (!found) return;
        sender!.CancelTimeout();
    }

    public void TryDisconnect(int connectionNumber)
    {
        _dataSenders.TryRemove(connectionNumber, out _);
    }
}