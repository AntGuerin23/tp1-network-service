using tp1_network_service.Builders;
using tp1_network_service.Enum;
using tp1_network_service.Packets;
using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Visitors;

namespace tp1_network_service.Primitives;

public class ConnectPrimitive : ConnectionPrimitive
{
    public ConnectPrimitive(int connectionNumber, CommunicationType type, int sourceAddress, int destinationAddress) 
        : base(connectionNumber, type, sourceAddress, destinationAddress)
    {
    }

    public override void Accept(IPacketVisitor packetVisitor)
    {
        var packet = new PacketBuilder()
            .ConnectionNumber(ConnectionNumber)
            .Type(Type)
            .SourceAddress(SourceAddress)
            .DestinationAddress(DestinationAddress)
            .ToConnectPacket();
        packetVisitor.Visit(packet);
    }
    
    public bool IsResponse() => Type == CommunicationType.Response;
    
    public bool IsConfirmation() => Type == CommunicationType.Confirmation;
}