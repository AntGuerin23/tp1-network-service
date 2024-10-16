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
    
    public PacketBuilder SetSegmentationInfo(SegmentationInfo segInfo)
    {
        _segInfo = segInfo;
        return this;
    }
    
    public ConnectionRequestPacket ToConnectionRequestPacket()
    {
        return new ConnectionRequestPacket(_type, ConnectionNumber, SourceAddress, DestinationAddress);
    }
    
    public ConnectionConfirmationPacket ToConnectionConfirmationPacket()
    {
        return new ConnectionConfirmationPacket(_type, ConnectionNumber, SourceAddress, DestinationAddress);
    }

    public DataPacket ToDataPacket()
    {
        return new DataPacket(_type, ConnectionNumber, Data, _segInfo);
    }
    
    public DataAcknowledgmentPacket ToDataAcknowledgmentPacket()
    {
        return new DataAcknowledgmentPacket(_type, ConnectionNumber, _segInfo);
    }

    public DisconnectPacket ToDisconnectPacket()
    {
        return new DisconnectPacket(_type, ConnectionNumber, SourceAddress, DestinationAddress, Reason);
    }
}