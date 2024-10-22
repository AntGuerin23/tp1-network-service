using tp1_network_service.External.Exceptions;
using tp1_network_service.Internal.Packets.Abstract;

namespace tp1_network_service.Internal.Packets.Children;

internal class ConnectionConfirmationPacket : AddressedPacket
{
    public ConnectionConfirmationPacket(PacketType type, int connectionNumber, int sourceAddress, int destinationAddress) : base(type, connectionNumber ,sourceAddress, destinationAddress) { }
    
    public override byte[] Serialize()
    {
        throw new UserBNotImplementedException();
    }

    public override void Handle()
    {
        //todo
        // var waitingConnection = NetworkLayer.Instance.GetAndResetWaitingConnectionNumberAndDestination();
        // if (waitingConnection != null && waitingConnection.Value.Item1 != ConnectionNumber)
        // {
        //     var disconnectPrimitive = new PrimitiveBuilder().SetConnectionNumber(waitingConnection.Value.Item1)
        //         .SetSourceAddress(waitingConnection.Value.Item2)
        //         .SetDestinationAddress(DestinationAddress)
        //         .SetType(PrimitiveType.Ind)
        //         .ToDisconnectPrimitive();
        //     TransportLayer.Instance.Disconnect(disconnectPrimitive);
        // }
        //
        // var connectPrimitive = new PrimitiveBuilder().SetConnectionNumber(ConnectionNumber)
        //     .SetSourceAddress(SourceAddress)
        //     .SetDestinationAddress(DestinationAddress)
        //     .SetType(PrimitiveType.Conf)
        //     .ToConnectPrimitive();
        // TransportLayer.Instance.ConfirmConnection(connectPrimitive);
    }
}