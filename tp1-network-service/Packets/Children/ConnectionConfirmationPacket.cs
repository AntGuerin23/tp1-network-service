using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Packets.Children;

internal class ConnectionConfirmationPacket : AddressedPacket
{
    public ConnectionConfirmationPacket(PacketType type, int connectionNumber, int sourceAddress, int destinationAddress) : base(type, connectionNumber ,sourceAddress, destinationAddress) { }
    
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    public override void Handle()
    {
        throw new NotImplementedException();
    }

}