namespace tp1_network_service.Packets.Children;

internal class ConnectionPacket : Packet
{
    public int? SourceAddress { get; set; }
    public int? DestinationAddress { get; set; }

}