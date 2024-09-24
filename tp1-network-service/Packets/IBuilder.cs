using tp1_network_service.Messages;

namespace tp1_network_service.Packets;

public interface IBuilder
{ 
    public void BuildPacket(string packetId);
    
    public void SetSource(int source);
    
    public void SetDestination(int destination);

    public Packet GetResult();
}
