using tp1_network_service.Enum;
using tp1_network_service.Packets;
using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Utils;
using tp1_network_service.Visitors;

namespace tp1_network_service.Primitives;

public class DataPrimitive : Primitive
{
    public byte[] Data { get; set; }
    
    public DataPrimitive(int connectionNumber, CommunicationType type, byte[] data) : base(connectionNumber, type)
    {
        Data = data;
    }

    public override void Accept(IPacketVisitor packetVisitor)
    {
        var packets = DataSegmenter.SegmentDataPrimitive(this);
        packetVisitor.Visit(packets);
    }
}