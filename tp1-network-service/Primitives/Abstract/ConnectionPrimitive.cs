using tp1_network_service.Enum;

namespace tp1_network_service.Primitives.Abstract;

public abstract class ConnectionPrimitive : Primitive
{
    public int SourceAddress { get; }
    public int DestinationAddress { get; }

    protected ConnectionPrimitive(int connectionNumber, CommunicationType type, int sourceAddress, int destinationAddress)
        : base(connectionNumber, type)
    {
        SourceAddress = sourceAddress;
        DestinationAddress = destinationAddress;
    }
}