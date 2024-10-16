using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Packets.Children;

internal class DataPacket : Packet
{
    public byte[] Data { get; set; }
    public SegmentationInfo SegInfo { get; set; }
    
    public DataPacket(PacketType type, byte[] data, SegmentationInfo segInfo) : base(type)
    {
        Data = data;
        SegInfo = segInfo;
    }
    
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    public override void Handle()
    {
        throw new NotImplementedException();
    }
}