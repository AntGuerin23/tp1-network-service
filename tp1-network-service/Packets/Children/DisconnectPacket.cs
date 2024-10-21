using tp1_network_service.Builder;
using tp1_network_service.Enums;
using tp1_network_service.Layers;
using tp1_network_service.Layers.Transport;
using tp1_network_service.Packets.Abstract;
using tp1_network_service.Primitives;

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
        var disconnectPrimitive = new PrimitiveBuilder().SetConnectionNumber(ConnectionNumber)
            .SetResponseAddress(SourceAddress)
            .SetType(PrimitiveType.Ind)
            .SetReason(DisconnectReason.Distant)
            .ToDisconnectPrimitive();
        TransportLayer.Instance.HandleFromLayer(disconnectPrimitive);
    }
}