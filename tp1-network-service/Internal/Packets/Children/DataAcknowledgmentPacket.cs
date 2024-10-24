using tp1_network_service.Internal.Layers.Network;
using tp1_network_service.Internal.Packets.Abstract;
using tp1_network_service.Internal.Packets.Segmentation;

namespace tp1_network_service.Internal.Packets.Children;

internal class DataAcknowledgmentPacket : Packet
{
    public bool ReceptionSuccess { get; set; }
    public byte NextSegmentNumber { get; set; }
    
    public DataAcknowledgmentPacket(PacketType type, int connectionNumber, SegmentationInfo segInfo) : base(type, connectionNumber)
    {
        ReceptionSuccess = segInfo.CurrentSegmentNumber != 4;
        NextSegmentNumber = ReceptionSuccess ? segInfo.NextSegmentNumber : segInfo.CurrentSegmentNumber;
    }
    
    public override byte[] Serialize()
    {
        return
        [
            (byte)ConnectionNumber,
            SegmentationInfo.SerializeAcknowledgment(ReceptionSuccess, NextSegmentNumber)
        ];
    }

    public override void Handle()
    {
        if (ReceptionSuccess)
        {
            NetworkLayer.Instance.DataSendingManager.AcknowledgeLastPacket(ConnectionNumber);
        }
    }
}