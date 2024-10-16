namespace tp1_network_service.Packets.Children;

internal abstract class AddressedPacket : Packet
{
    public int? SourceAddress { get; set; }
    public int? DestinationAddress { get; set; }
}