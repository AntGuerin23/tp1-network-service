using tp1_network_service.Enums;

namespace tp1_network_service.Messages.Builder;

internal interface IBuilder
{ 
    public void Reset();

    public void SetType(MessageType type);
    
    public void SetPrimitive (MessagePrimitive primitive);
    
    public void SetSource(byte source);
    
    public void SetDestination(byte destination);

    public Message GetResult();
}
