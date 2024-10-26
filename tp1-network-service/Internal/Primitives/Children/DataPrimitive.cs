using tp1_network_service.Internal.Primitives.Abstract;

namespace tp1_network_service.Internal.Primitives.Children;

internal class DataPrimitive : Primitive
{
    public byte[] Data { get; }

    public DataPrimitive(PrimitiveType type, int connectionNumber, byte[] data) : base(connectionNumber, type)
    {
        Data = data;
    }
}