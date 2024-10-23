using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.Layers.Transport;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Primitives;

namespace tp1_network_service.Internal.Packets.Children;

internal class DisconnectPacket : AddressedPacket
{
    public DisconnectReason Reason { get; set; }
    
    public DisconnectPacket(PacketType type, int connectionNumber, int sourceAddress, int destinationAddress, DisconnectReason reason) : base(type, connectionNumber, sourceAddress, destinationAddress)
    {
        Reason = reason;
    }
    
    public override byte[] Serialize()
    {
        return 
        [
            (byte)ConnectionNumber,
            (byte)Type,
            (byte)SourceAddress,
            (byte)DestinationAddress,
            (byte)Reason
        ];
    }

    public override void Handle()
    {
        var disconnectPrimitive = new PrimitiveBuilder().SetConnectionNumber(ConnectionNumber)
            .SetResponseAddress(SourceAddress)
            .SetType(PrimitiveType.Ind)
            .SetReason(DisconnectReason.Distant)
            .ToDisconnectPrimitive();
        TransportLayer.Instance.HandleFromLayer(disconnectPrimitive);
    }
}