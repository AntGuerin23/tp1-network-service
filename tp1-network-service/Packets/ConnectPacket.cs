using tp1_network_service.Enum;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Packets;

public class ConnectPacket : ConnectionPacket
{
    public ConnectPacket(int connectionNumber, CommunicationType type, int sourceAddress, int destinationAddress)
        : base(connectionNumber, type, sourceAddress, destinationAddress)
    {
    }

    public bool IsResponse() => Type == CommunicationType.Response;
    public bool IsConfirmation() => Type == CommunicationType.Confirmation;
    
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }
}