using tp1_network_service.Messages;

namespace tp1_network_service.Packets.Connect;

public class ConnectedPacket : Packet
{
    public const string Id = "00001111";
    
    public ConnectedPacket()
    {
        PacketType = Id;
    }
}
