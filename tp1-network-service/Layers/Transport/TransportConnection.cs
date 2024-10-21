using tp1_network_service.Enums;

namespace tp1_network_service.Layers.Transport;

internal class TransportConnection
{
    public TransportConnectionStatus Status { get; set; }
    public byte[] PendingData { get; set; }

    public TransportConnection(TransportConnectionStatus status)
    {
        Status = status;
        PendingData = [];
    }
}
