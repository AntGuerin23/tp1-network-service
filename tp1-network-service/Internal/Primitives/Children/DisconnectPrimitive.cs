using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.Packets;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Primitives.Abstract;

namespace tp1_network_service.Internal.Primitives.Children; 

internal class DisconnectPrimitive : AddressedPrimitive
{
    public DisconnectReason Reason { get; }
    
    public DisconnectPrimitive(PrimitiveType type, int connectionNumber, DisconnectReason reason, int source, int destination) : base(connectionNumber, type, source, destination)
    {
        Reason = reason;
    }

    public Packet GeneratePacket()
    {
        return new PacketBuilder().SetConnectionNumber(ConnectionNumber)
            .SetType(PacketType.Disconnect)
            .SetSourceAddress(SourceAddress)
            .SetDestinationAddress(DestinationAddress)
            .SetReason(Reason)
            .ToDisconnectPacket();
    }
}