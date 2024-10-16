namespace tp1_network_service.Packets.Abstract;

internal abstract class AddressedPacket : Packet
{
    public AddressedPacket(PacketType type, int sourceAddress, int destinationAddress) : base(type)
    {
        SourceAddress = sourceAddress;
        DestinationAddress = destinationAddress;
    }
    
    public int SourceAddress { get; set; }
    public int DestinationAddress { get; set; }
}