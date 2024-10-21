using tp1_network_service.Enum;
using tp1_network_service.Primitives;

namespace tp1_network_service.Builders;

public class PrimitiveBuilder
{
    // Primitive fields
    private int _connectionNumber;
    private CommunicationType _type;
    
    // ConnectionPrimitive fields
    private int _sourceAddress;
    private int _destinationAddress;
    
    // DisconnectPrimitive fields
    private DisconnectReason _reason;
    
    // DataPrimitive fields
    private byte[] _data;

    public PrimitiveBuilder ConnectionNumber(int connectionNumber)
    {
        _connectionNumber = connectionNumber;
        return this;
    }

    public PrimitiveBuilder Type(CommunicationType type)
    {
        _type = type;
        return this;
    }

    public PrimitiveBuilder SourceAddress(int sourceAddress)
    {
        _sourceAddress = sourceAddress;
        return this;
    }

    public PrimitiveBuilder DestinationAddress(int destinationAddress)
    {
        _destinationAddress = destinationAddress;
        return this;
    }

    public PrimitiveBuilder Reason(DisconnectReason reason)
    {
        _reason = reason;
        return this;
    }

    public PrimitiveBuilder Data(byte[] data)
    {
        _data = data;
        return this;
    }

    public ConnectPrimitive ToConnectPrimitive()
    {
        return new ConnectPrimitive(_connectionNumber, _type, _sourceAddress, _destinationAddress);
    }

    public DisconnectPrimitive ToDisconnectPrimitive()
    {
        return new DisconnectPrimitive(_connectionNumber, _type, _sourceAddress, _destinationAddress, _reason);
    }

    public DataPrimitive ToDataPrimitive()
    {
        return new DataPrimitive(_connectionNumber, _type,  _data);
    }
}