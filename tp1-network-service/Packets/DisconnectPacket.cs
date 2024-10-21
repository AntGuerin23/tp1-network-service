using tp1_network_service.Enum;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Packets;

public class DisconnectPacket : ConnectionPacket
{

    public DisconnectReason Reason { get; }

    public DisconnectPacket(int connectionNumber, CommunicationType type, int sourceAddress, int destinationAddress, DisconnectReason reason) 
        : base(connectionNumber, type, sourceAddress, destinationAddress)
    {
        Reason = reason;
    }

    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }
}