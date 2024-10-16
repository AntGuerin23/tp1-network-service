using tp1_network_service.Builder.Abstract;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Children;

namespace tp1_network_service.Builder;

internal class PacketBuilder : CommunicationEntityBuilder<PacketBuilder>
{
    private PacketType _type;
    private SegmentationInfo _segInfo;
    
    public PacketBuilder SetType(PacketType type)
    {
        _type = type;
        return this;
    }
    
    public ConnectionRequestPacket ToConnectionRequestPacket()
    {
        return new ConnectionRequestPacket(_type, SourceAddress, DestinationAddress);
    }

    public DataPacket ToDataPacket()
    {
        return new DataPacket(_type, Data, _segInfo);
    }

    public DisconnectPacket ToDisconnectPacket()
    {
        return new DisconnectPacket(_type, SourceAddress, DestinationAddress, Reason);
    }
}