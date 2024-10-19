using tp1_network_service.Builder;
using tp1_network_service.Packets.Children;
using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Packets;

internal class PacketSegmenter
{
    private const int MaxPacketSize = 128;
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
        //todo : create segmentation info and segment (data)
        
        SegmentationIndex++;
        return new PacketBuilder().SetConnectionNumber(Primitive.ConnectionNumber)
            .SetType(PacketType.Data)
            .SetSegmentationInfo(new SegmentationInfo())
            .SetData(Data)
            .ToDataPacket();
    }

    public void WaitForAcknowledgementTimeout()
    {
        _segmentationIndexOnTimeoutStart = SegmentationIndex;
        //TODO : wait timer
    }

    public bool LastSegmentWasAcknowledged()
    {
        return SegmentationIndex == _segmentationIndexOnTimeoutStart;
    }

    public bool PacketNeedsToBeSegmented()
    {
        return Primitive.Data.Length > MaxPacketSize;
    }
}