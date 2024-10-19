using tp1_network_service.Builder;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Primitives.Children;

internal class ConnectPrimitive : Primitive
{
    public int SourceAddress { get; }
    public int DestinationAddress { get; }

    public int ResponseAddress
    {
        get
        {
            if (Type is PrimitiveType.Resp or PrimitiveType.Conf)
            {
                return DestinationAddress;
            }
            return default;
        }
    }

    public ConnectPrimitive(PrimitiveType type, int connectNumber, int source, int destination) : base(type, connectNumber)
    {
        SourceAddress = source;
        DestinationAddress = destination;
    }

    public Packet GeneratePacket()
    {
        return new PacketBuilder().SetConnectionNumber(ConnectionNumber)
            .SetType(PacketType.ConnectRequest)
            .SetSourceAddress(SourceAddress)
            .SetDestinationAddress(DestinationAddress)
            .ToConnectionRequestPacket();
    }
}