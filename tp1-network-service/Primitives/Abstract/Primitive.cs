using tp1_network_service.Enum;
using tp1_network_service.Visitors;

namespace tp1_network_service.Primitives.Abstract;

public abstract class Primitive
{
    public int ConnectionNumber { get; }
    public CommunicationType Type { get; private set; }

    protected Primitive(int connectionNumber, CommunicationType type)
    {
        ConnectionNumber = connectionNumber;
        Type = type;
    }
    
    public abstract void Accept(IPacketVisitor packetVisitor);
    
    public bool IsRequest() => Type == CommunicationType.Request;
    public bool IsIndication() => Type == CommunicationType.Indication;

    public Primitive ToIndication()
    {
        Type = CommunicationType.Indication;
        return this;
    }
}