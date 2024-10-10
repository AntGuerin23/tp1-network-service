namespace tp1_network_service.Packets.Children;

internal class LiberationPacket : Packet
{
    public DisconnectReason? Reason { get; set; }
}