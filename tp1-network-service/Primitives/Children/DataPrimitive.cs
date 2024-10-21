using tp1_network_service.Builder;
using tp1_network_service.Layers;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Abstract;
using tp1_network_service.Primitives.Abstract;

namespace tp1_network_service.Primitives.Children;

internal class DataPrimitive : Primitive
{
    public byte[] Data { get; }

    public DataPrimitive(PrimitiveType type, int connectionNumber, byte[] data) : base(type, connectionNumber)
    {
        Data = data;
    }
}