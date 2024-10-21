using tp1_network_service.Enum;

namespace tp1_network_service.Packets.Abstract;

public abstract class Packet
{
    public int ConnectionNumber { get; }
    public CommunicationType Type { get; }

    protected Packet(int connectionNumber, CommunicationType type)
    {
        ConnectionNumber = connectionNumber;
        Type = type;
    }

    public bool IsRequest() => Type == CommunicationType.Request;
    public bool IsIndication() => Type == CommunicationType.Indication;
    public abstract byte[] Serialize();
}