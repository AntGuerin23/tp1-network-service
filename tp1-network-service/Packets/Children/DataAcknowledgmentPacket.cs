using tp1_network_service.Exceptions;
using tp1_network_service.Layers;
using tp1_network_service.Layers.Network;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Packets.Children;

internal class DataAcknowledgmentPacket : Packet
{
    public bool ReceptionSuccess { get; set; }
    public byte NextSegmentNumber { get; set; }
    
    public DataAcknowledgmentPacket(PacketType type, int connectionNumber, SegmentationInfo segInfo) : base(type, connectionNumber)
    {
        ReceptionSuccess = segInfo.CurrentSegmentNumber != 4;
        NextSegmentNumber = segInfo.NextSegmentNumber;
    }
    
    public override byte[] Serialize()
    {
        throw new UserBNotImplementedException();
    }

    public override void Handle()
    {
        if (ReceptionSuccess)
        {
            NetworkLayer.Instance.PendingSendingDataManager.AcknowledgeLastPacket();
        }
    }
}