using tp1_network_service.Builder;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Primitives.Children;

internal class DataPrimitive : Primitive
{
    public byte[] Data { get; }

    public DataPrimitive(PrimitiveType type, int connectionNumber, byte[] data) : base(type, connectionNumber)
    {
        Data = data;
    }

    public override Packet ToPacket()
    {
        return new PacketBuilder().SetConnectionNumber(ConnectionNumber)
            .SetType(PacketType.Data)
            .SetData(Data)
            .ToDataPacket();
    }
}