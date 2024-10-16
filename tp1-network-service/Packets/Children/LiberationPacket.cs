namespace tp1_network_service.Packets.Children;

internal class LiberationPacket : AddressedPacket
{
    public DisconnectReason? Reason { get; set; }
    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }

    public override void Handle()
    {
        throw new NotImplementedException();
    }
}