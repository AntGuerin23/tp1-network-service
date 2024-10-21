using tp1_network_service.Internal.Enums;

namespace tp1_network_service.Internal.Builder.Abstract;

internal abstract class CommunicationEntityBuilder<T> where T : CommunicationEntityBuilder<T>
{
    protected int ConnectionNumber;
    
    protected int SourceAddress;
    protected int DestinationAddress;
    protected int ResponseAddress;

    protected byte[] Data;

    protected DisconnectReason Reason;
    
    public T SetConnectionNumber(int connectionNumber)
    {
        ConnectionNumber = connectionNumber;
        return (T) this;
    }

    public T SetSourceAddress(int sourceAddress)
    {
        SourceAddress = sourceAddress;
        return (T) this;
    }

    public T SetDestinationAddress(int destinationAddress)
    {
        DestinationAddress = destinationAddress;
        return (T) this;
    }

    public T SetResponseAddress(int responseAddress)
    {
        ResponseAddress = responseAddress;
        return (T) this;
    }

    public T SetData(byte[] data)
    {
        Data = data;
        return (T) this;
    }

    public T SetReason(DisconnectReason reason)
    {
        Reason = reason;
        return (T) this;
    }
}