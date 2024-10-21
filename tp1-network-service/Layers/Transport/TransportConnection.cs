using tp1_network_service.Enum;

namespace tp1_network_service.Layers.Transport;

public class TransportConnection
{
    public TransportConnectionStatus Status { get; set; }
    public byte[] PendingData { get; set; }

    public TransportConnection(TransportConnectionStatus status)
    {
        Status = status;
        PendingData = [];
    }
}
