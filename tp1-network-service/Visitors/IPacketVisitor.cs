using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Visitors;

public interface IPacketVisitor
{
    void Visit(Packet packet);
    void Visit(IEnumerable<Packet> packets);
}