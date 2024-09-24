using tp1_network_service.Messages;

namespace tp1_network_service.Packets.Disconnect;

public class LiberationPacket : Packet
{
    public const string Id = "00010011";
    
    public LiberationPacket()
    {
        PacketType = Id;
    }
}
