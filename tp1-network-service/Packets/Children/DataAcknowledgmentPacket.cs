using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Packets.Children;

internal class DataAcknowledgmentPacket : Packet
{
    public bool ReceptionSuccess { get; set; }
    public byte NextSegmentNumber { get; set; }
    
    public DataAcknowledgmentPacket(PacketType type, bool receptionSuccess, byte nextSegmentNumber) : base(type)
    {
        ReceptionSuccess = receptionSuccess;
        NextSegmentNumber = nextSegmentNumber;
    }
    
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    public override void Handle()
    {
        throw new NotImplementedException();
    }
}