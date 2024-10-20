using tp1_network_service.Builder;
using tp1_network_service.Layers;
using tp1_network_service.Packets.Children;
using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Packets;

internal class PacketSegmenter
{
    private const int MaxPacketSizeBytes = 128;
    private const int AcknowledgementTimeoutSeconds = 5;

    public DataPrimitive Primitive { get; set; }

    public int SegmentationIndex
    {
        get
        {
            lock (_segmentationIndexLock)
            {
                return _segmentationIndex;
            }
        }
        set
        {
            lock (_segmentationIndexLock)
            {
                _segmentationIndex = value;
            }
        }
    }

    private int _segmentationIndex;
    private int _segmentationIndexOnTimeoutStart;
    private object _segmentationIndexLock = new();


    public PacketSegmenter(DataPrimitive primitive)
    {
        Primitive = primitive;
        SegmentationIndex = 0;
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

    public void WaitForAcknowledgementTimeout()
    {
        _segmentationIndexOnTimeoutStart = SegmentationIndex;
        Thread.Sleep(AcknowledgementTimeoutSeconds * 1000);
    }

    public bool LastSegmentWasAcknowledged()
    {
        return SegmentationIndex == _segmentationIndexOnTimeoutStart;
    }

    public bool PacketNeedsToBeSegmented()
    {
        return Primitive.Data.Length > MaxPacketSizeBytes;
    }

    private SegmentationInfo BuildSegmentationInfo()
    {
        var currentPacketNumber = (byte)(SegmentationIndex % 8);
        if (SegmentationIndex + MaxPacketSizeBytes >= Primitive.Data.Length)
        {
            NetworkLayer.Instance.CancelTimeout();
            return new SegmentationInfo(currentPacketNumber, false);
        }
        return new SegmentationInfo(currentPacketNumber, true, (byte)(currentPacketNumber + 1));
    }

    private byte[] BuildSegment()
    {
        var segment = new byte[MaxPacketSizeBytes];
        Array.Copy(Primitive.Data, SegmentationIndex, segment, 0, MaxPacketSizeBytes);
        return segment;
    }
}