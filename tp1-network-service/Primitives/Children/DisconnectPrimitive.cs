using System.Diagnostics;
using tp1_network_service.Builder;
using tp1_network_service.Enums;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Abstract;
using tp1_network_service.Primitives.Abstract;

namespace tp1_network_service.Primitives.Children; 

internal class DisconnectPrimitive : Primitive
{
    public int SourceAddress { get; private set; }
    public int DestinationAddress { get; private set; }
    
    public DisconnectReason Reason { get; }
    
    public DisconnectPrimitive(PrimitiveType type, int connectionNumber, DisconnectReason reason, int source, int destination) : base(type, connectionNumber)
    {
        Reason = reason;
        SourceAddress = source;
        DestinationAddress = destination;
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