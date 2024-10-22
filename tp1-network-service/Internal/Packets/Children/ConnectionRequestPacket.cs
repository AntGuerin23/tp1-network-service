using tp1_network_service.External.Exceptions;
using tp1_network_service.Internal.Packets.Abstract;

namespace tp1_network_service.Internal.Packets.Children;

internal class ConnectionRequestPacket : AddressedPacket
{
    public ConnectionRequestPacket(PacketType type, int connectionNumber, int sourceAddress, int destinationAddress) : base(type, connectionNumber, sourceAddress, destinationAddress) { }
    
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    public override void Handle()
    {
        throw new UserBNotImplementedException();
    }
}