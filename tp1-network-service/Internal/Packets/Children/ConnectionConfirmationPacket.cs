using tp1_network_service.External.Exceptions;
using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.Layers.Network;
using tp1_network_service.Internal.Layers.Transport;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Primitives;

namespace tp1_network_service.Internal.Packets.Children;

internal class ConnectionConfirmationPacket : AddressedPacket
{
    public ConnectionConfirmationPacket(PacketType type, int connectionNumber, int sourceAddress, int destinationAddress) : base(type, connectionNumber ,sourceAddress, destinationAddress) { }
    
    public override byte[] Serialize()
    {
        return
        [
            (byte)ConnectionNumber,
            (byte)Type,
            (byte)SourceAddress,
            (byte)DestinationAddress
        ];
    }

    public override void Handle()
    {
        var receivedBeforeTimeout = NetworkLayer.Instance.PendingConnectionRequestManager.ConfirmConnection(ConnectionNumber);
        if (!receivedBeforeTimeout) return;
        
        var connectPrimitive = new PrimitiveBuilder()
            .SetConnectionNumber(ConnectionNumber)
            .SetResponseAddress(DestinationAddress)
            .SetType(PrimitiveType.Conf)
            .ToConnectPrimitive();
        TransportLayer.Instance.HandleFromLayer(connectPrimitive);
    }
}