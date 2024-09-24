using tp1_network_service.Messages;

namespace tp1_network_service.Packets.Data;

public class DataPacket : Packet
{
    public DataPacket(string packetType)
    {
        PacketType = packetType;
    }
}
