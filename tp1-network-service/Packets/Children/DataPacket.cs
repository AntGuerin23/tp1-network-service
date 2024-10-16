namespace tp1_network_service.Packets.Children;

internal class DataPacket : Packet
{
    public byte[] Data { get; set; }
    public SegmentationInfo SegInfo { get; set; }
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    public override void Handle()
    {
        throw new NotImplementedException();
    }
}