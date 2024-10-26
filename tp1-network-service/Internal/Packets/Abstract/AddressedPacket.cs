namespace tp1_network_service.Internal.Packets.Abstract;

internal abstract class AddressedPacket : Packet
{
    public AddressedPacket(PacketType type, int connectionNumber, int sourceAddress, int destinationAddress) : base(type, connectionNumber)
    {
        SourceAddress = sourceAddress;
        DestinationAddress = destinationAddress;
    }
    
    public int SourceAddress { get; set; }
    public int DestinationAddress { get; set; }
}