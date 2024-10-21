using tp1_network_service.Builders;
using tp1_network_service.Enum;
using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Visitors;

namespace tp1_network_service.Primitives;

public class DisconnectPrimitive : ConnectionPrimitive
{
    public DisconnectReason Reason { get; }
    
    public DisconnectPrimitive(int connectionNumber, CommunicationType type, int sourceAddress, int destinationAddress, DisconnectReason reason) 
        : base(connectionNumber, type, sourceAddress, destinationAddress)
    {
        Reason = reason;
    }

    public override void Accept(IPacketVisitor packetVisitor)
    {
        var packet = new PacketBuilder()
            .ConnectionNumber(ConnectionNumber)
            .Type(Type)
            .SourceAddress(SourceAddress)
            .DestinationAddress(DestinationAddress)
            .Reason(Reason)
            .ToDisconnectPacket();
        packetVisitor.Visit(packet);
    }
}