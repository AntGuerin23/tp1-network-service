using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Builder;

internal class PrimitiveBuilder
{
    private PrimitiveType _type;
    private int _connectionNumber;
    
    private int _sourceAddress;
    private int _destinationAddress;
    private int _responseAddress;


    private byte[] _data;

    private DisconnectReason _reason;


    public ConnectPrimitive ToConnectPrimitive()
    {
        return new ConnectPrimitive(_type, _connectionNumber, _sourceAddress, _destinationAddress);
    }

    public DataPrimitive ToDataPrimitive()
    {
        return new DataPrimitive(_type, _connectionNumber, _data);
    }

    public DisconnectPrimitive ToDisconnectPrimitive()
    {
        return new DisconnectPrimitive(_type, _connectionNumber, _reason);
    }

    public PrimitiveBuilder SetType(PrimitiveType type)
    {
        _type = type;
        return this;
    }

    public PrimitiveBuilder SetConnectionNumber(int connectionNumber)
    {
        _connectionNumber = connectionNumber;
        return this;
    }

    public PrimitiveBuilder SetSourceAddress(int sourceAddress)
    {
        _sourceAddress = sourceAddress;
        return this;
    }

    public PrimitiveBuilder SetDestinationAddress(int destinationAddress)
    {
        _destinationAddress = destinationAddress;
        return this;
    }

    public PrimitiveBuilder SetResponseAddress(int responseAddress)
    {
        _responseAddress = responseAddress;
        return this;
    }

    public PrimitiveBuilder SetData(byte[] data)
    {
        _data = data;
        return this;
    }

    public PrimitiveBuilder SetReason(DisconnectReason reason)
    {
        _reason = reason;
        return this;
    }
}