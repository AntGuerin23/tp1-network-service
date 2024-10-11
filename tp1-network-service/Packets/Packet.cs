namespace tp1_network_service.Packets;

internal abstract class Packet
{
    public int ConnectionNumber { get; set; }
    public PacketType Type { get; set; }
}