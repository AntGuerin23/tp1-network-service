using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Packets;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Primitives.Abstract;

namespace tp1_network_service.Internal.Primitives.Children;

internal class ConnectPrimitive : AddressedPrimitive
{
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
    
    public bool IsResponse() => Type == PrimitiveType.Resp;
    public bool IsConfirmation() => Type == PrimitiveType.Conf;


    public ConnectPrimitive(PrimitiveType type, int connectNumber, int source, int destination) : base(connectNumber, type, source, destination) { }
    


    public Packet GeneratePacket()
    {
        return new PacketBuilder().SetConnectionNumber(ConnectionNumber)
            .SetType(PacketType.ConnectRequest)
            .SetSourceAddress(SourceAddress)
            .SetDestinationAddress(DestinationAddress)
            .ToConnectionRequestPacket();
    }
}