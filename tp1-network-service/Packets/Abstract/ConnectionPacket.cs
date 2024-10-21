using tp1_network_service.Enum;

namespace tp1_network_service.Packets.Abstract;

public abstract class ConnectionPacket : Packet
{
    
    public int SourceAddress { get; set; }
    public int DestinationAddress { get; set; }
    
    protected ConnectionPacket(int connectionNumber, CommunicationType type, int sourceAddress, int destinationAddress) 
        : base(connectionNumber, type)
    {
        SourceAddress = sourceAddress;
        DestinationAddress = destinationAddress;
    }
}