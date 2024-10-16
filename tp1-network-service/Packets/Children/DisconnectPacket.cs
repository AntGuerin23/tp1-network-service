using tp1_network_service.Enums;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Packets.Children;

internal class DisconnectPacket : AddressedPacket
{
    public DisconnectReason? Reason { get; set; }
    
    public DisconnectPacket(PacketType type, int connectionNumber, int sourceAddress, int destinationAddress, DisconnectReason? reason) : base(type, connectionNumber, sourceAddress, destinationAddress)
    {
        Reason = reason;
    }
    
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    public override void Handle()
    {
        throw new NotImplementedException();
    }
}