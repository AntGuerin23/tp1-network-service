namespace tp1_network_service.Packets.Children;

internal class DataAcknowledgmentPacket : Packet
{
    public bool ReceptionSuccess { get; set; }
    public byte NextSegmentNumber { get; set; }
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    public override void Handle()
    {
        throw new NotImplementedException();
    }
}