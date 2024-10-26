using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Packets.Children;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Packets.Segmentation;

internal class PacketSegmenter
{
    private const int MaxPacketSizeBytes = 128;

    public bool HasNextSegment { get; set; }
    public DataPrimitive Primitive { get; set; }
    public int SegmentationIndex { get; set; }

    public PacketSegmenter(DataPrimitive primitive)
    {
        Primitive = primitive;
        SegmentationIndex = 0;
        HasNextSegment = true;
    }
    
    public DataPacket ConstructNextPacket()
    {
        var builder = new PacketBuilder().SetConnectionNumber(Primitive.ConnectionNumber)
            .SetType(PacketType.Data)
            .SetSegmentationInfo(BuildSegmentationInfo())
            .SetData(BuildSegment());
        SegmentationIndex += MaxPacketSizeBytes;
        return builder.ToDataPacket();
        
    }

    private SegmentationInfo BuildSegmentationInfo()
    {
        var currentPacketNumber = (byte)(SegmentationIndex % 8);
        if (SegmentationIndex + MaxPacketSizeBytes >= Primitive.Data.Length)
        {
            HasNextSegment = false;
            return new SegmentationInfo(0, HasNextSegment, currentPacketNumber);
        }

        HasNextSegment = true;
        return new SegmentationInfo(currentPacketNumber, HasNextSegment, (byte)(currentPacketNumber + 1));
    }

    private byte[] BuildSegment()
    {
        var length = Math.Min(MaxPacketSizeBytes, Primitive.Data.Length - SegmentationIndex);
        var segment = new byte[length];
        Array.Copy(Primitive.Data, SegmentationIndex, segment, 0, length);
        return segment;
    }
}