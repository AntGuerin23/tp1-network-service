using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Network.DataSending;

internal class DataSendingManager
{
    private readonly Dictionary<int, DataSender> _dataSenders = new();
    private readonly object _dataSendersLock = new();

    public bool StartSendingData(DataPrimitive primitive)
    {
        var sender = new DataSender(primitive);
        lock (_dataSendersLock)
        {
            _dataSenders.Add(primitive.ConnectionNumber, sender);
        }
        return sender.SendData();
    }
    
    public void AcknowledgeLastPacket(int connectionNumber)
    {
        lock (_dataSendersLock)
        {
            _dataSenders[connectionNumber].Acknowledge();
        }
    }
}