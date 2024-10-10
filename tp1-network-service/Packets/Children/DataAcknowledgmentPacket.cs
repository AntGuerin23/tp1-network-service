namespace tp1_network_service.Packets.Children;

internal class DataAcknowledgmentPacket : Packet
{
    public bool ReceptionSuccess { get; set; }
    public byte NextSegmentNumber { get; set; }
}