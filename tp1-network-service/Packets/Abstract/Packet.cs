namespace tp1_network_service.Packets.Abstract;

internal abstract class Packet : CommunicationEntity
{
    protected Packet(PacketType type, int connectionNumber) : base(connectionNumber)
    {
        Type = type;
    }
    
    public PacketType Type { get; set; }

    public abstract byte[] Serialize();
    public abstract void Handle();
}