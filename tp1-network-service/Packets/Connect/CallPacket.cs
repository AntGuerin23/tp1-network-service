namespace tp1_network_service.Packets.Connect;

public class CallPacket : Packet
{
    public const string Id = "00001011";

    public CallPacket()
    {
        PacketType = Id;
    }
}
