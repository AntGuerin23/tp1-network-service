namespace tp1_network_service.Packets.Children;

internal class DataPacket : Packet
{
    public byte[] Data { get; set; }
    public SegmentationInfo SegInfo { get; set; }
}