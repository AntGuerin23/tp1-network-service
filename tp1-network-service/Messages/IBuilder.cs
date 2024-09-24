using tp1_network_service.Messages;

namespace tp1_network_service.Messages;

public interface IBuilder
{ 
    public void Reset();

    public void SetType();
    
    public void SetSource(int source);
    
    public void SetDestination(int destination);

    public Message GetResult();
}
