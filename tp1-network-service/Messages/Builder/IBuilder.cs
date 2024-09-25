using tp1_network_service.Enums;

namespace tp1_network_service.Messages.Builder;

internal interface IBuilder
{ 
    public void Reset();

    public void SetType(MessageType type);
    
    public void SetSource(int source);
    
    public void SetDestination(int destination);

    public Message GetResult();
}
