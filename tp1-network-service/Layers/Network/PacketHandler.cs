using tp1_network_service.Builders;
using tp1_network_service.Enum;
using tp1_network_service.Layers.Transport;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Layers.Network;

public class PacketHandler
{

    public void Handle(Packet packet)
    {
        switch (packet)
        {
            case ConnectPacket connect:
                HandleConnectPacket(connect);
                break;
            case DisconnectPacket disconnect:
                HandleDisconnectPacket(disconnect);
                break;
            case DataPacket data:
                HandleDataPacket(data);
                break;
        }
    }

    private void HandleConnectPacket(ConnectPacket connect)
    {
        if (!connect.IsResponse()) return;
        var primitive = new PrimitiveBuilder()
            .ConnectionNumber(connect.ConnectionNumber)
            .SourceAddress(connect.SourceAddress)
            .DestinationAddress(connect.DestinationAddress)
            .Type(CommunicationType.Confirmation)
            .ToConnectPrimitive();
        TransportLayer.Instance.HandleFromLayer(primitive);
    }

    private void HandleDisconnectPacket(DisconnectPacket disconnect)
    {
        if (!disconnect.IsRequest()) return;
        var primitive = new PrimitiveBuilder()
            .ConnectionNumber(disconnect.ConnectionNumber)
            .SourceAddress(disconnect.SourceAddress)
            .DestinationAddress(disconnect.DestinationAddress)
            .Reason(DisconnectReason.Distant)
            .Type(CommunicationType.Indication)
            .ToDisconnectPrimitive();
        TransportLayer.Instance.HandleFromLayer(primitive);
    }

    private void HandleDataPacket(DataPacket data)
    {
        throw new NotImplementedException();
    }

}