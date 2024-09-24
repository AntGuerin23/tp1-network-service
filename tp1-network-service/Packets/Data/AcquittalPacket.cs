namespace tp1_network_service.Packets.Data;

public class AcquittalPacket : Packet
{
    public AcquittalPacket(string packetType)
    {
        PacketType = packetType;
    }
}