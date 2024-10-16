namespace tp1_network_service.Primitives.Children;

internal class DataPrimitive : Primitive
{
    public byte[] Data { get; }

    public DataPrimitive(PrimitiveType type, int connectionNumber, byte[] data) : base(type, connectionNumber)
    {
        Data = data;
    }
}