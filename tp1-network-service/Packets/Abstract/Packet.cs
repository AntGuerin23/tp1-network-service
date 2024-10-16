namespace tp1_network_service.Packets;

internal abstract class Packet
{
    public int ConnectionNumber { get; set; }
    public PacketType Type { get; set; }

    public abstract byte[] Serialize();
    public abstract void Handle();
}