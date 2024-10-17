using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Primitives;

internal abstract class Primitive : CommunicationEntity
{
    public PrimitiveType Type { get; set; }
    
    protected Primitive(PrimitiveType type, int connectionNumber) : base(connectionNumber)
    {
        Type = type;
    }

    public abstract Packet ToPacket();
}