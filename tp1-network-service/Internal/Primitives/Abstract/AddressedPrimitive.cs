namespace tp1_network_service.Internal.Primitives.Abstract;

internal abstract class AddressedPrimitive : Primitive
{
    public int SourceAddress { get; }
    public int DestinationAddress { get; }

    protected AddressedPrimitive(int connectionNumber, PrimitiveType type, int sourceAddress, int destinationAddress)
        : base(connectionNumber, type)
    {
        SourceAddress = sourceAddress;
        DestinationAddress = destinationAddress;
    }
}