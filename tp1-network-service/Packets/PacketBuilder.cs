using tp1_network_service.Packets.Connect;
using tp1_network_service.Packets.Data;
using tp1_network_service.Packets.Disconnect;

namespace tp1_network_service.Packets;

public class PacketBuilder : IBuilder
{
    private Packet _packet;

    public void BuildPacket(string packetId)
    {
        switch (packetId)
        {
            case CallPacket.Id:
                _packet = new CallPacket();
                break;
            case ConnectedPacket.Id:
                _packet = new ConnectedPacket();
                break;
            case LiberationPacket.Id:
                _packet = new LiberationPacket();
                break;
            default:
                _packet = new DataPacket(packetId);
                break;
        }
    }

    public void SetSource(int source)
    {
        _packet.Source = source;
    }

    public void SetDestination(int destination)
    {
        _packet.Destination = destination;
    }

    public Packet GetResult()
    {
        return _packet;
    }
}
