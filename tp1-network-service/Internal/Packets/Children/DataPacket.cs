using tp1_network_service.External.Exceptions;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Packets.Segmentation;

namespace tp1_network_service.Internal.Packets.Children;

internal class DataPacket : Packet
{
    public byte[] Data { get; set; }
    public SegmentationInfo SegInfo { get; set; }
    
    public DataPacket(PacketType type, int connectionNumber, byte[] data, SegmentationInfo segInfo) : base(type, connectionNumber)
    {
        Data = data;
        SegInfo = segInfo;
    }
    
    public override byte[] Serialize()
    {
        var bytes = new List<byte>
        {
            (byte)ConnectionNumber,
            SegInfo.Serialize()
        };
        bytes.AddRange(Data);
        return bytes.ToArray();
    }

    public override void Handle()
    {
        throw new UserBNotImplementedException();
    }
}