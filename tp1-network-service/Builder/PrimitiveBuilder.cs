using tp1_network_service.Builder.Abstract;
using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Builder;

internal class PrimitiveBuilder : CommunicationEntityBuilder<PrimitiveBuilder>
{
    private PrimitiveType _type;
    
    public PrimitiveBuilder SetType(PrimitiveType type)
    {
        _type = type;
        return this;
    }
    
    public ConnectPrimitive ToConnectPrimitive()
    {
        return new ConnectPrimitive(_type, ConnectionNumber, SourceAddress, DestinationAddress);
    }

    public DataPrimitive ToDataPrimitive()
    {
        return new DataPrimitive(_type, ConnectionNumber, Data);
    }

    public DisconnectPrimitive ToDisconnectPrimitive()
    {
        return new DisconnectPrimitive(_type, ConnectionNumber, Reason);
    }
}