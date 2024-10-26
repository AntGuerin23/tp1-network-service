using tp1_network_service.Internal.Packets.Segmentation;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Network.DataSending;

internal class DataSender
{
    private const int AcknowledgementTimeoutSeconds = 1;
    
    private readonly PacketSegmenter _currentPacketSegmenter;
    private readonly Timeout _timeout;

    public DataSender(DataPrimitive primitive)
    {
        _timeout = new Timeout(AcknowledgementTimeoutSeconds);
        _currentPacketSegmenter = new PacketSegmenter(primitive);
    }

    public bool SendData()
    {
        var success = true;
        while (_currentPacketSegmenter.HasNextSegment)
        {
            var packet = _currentPacketSegmenter.ConstructNextPacket();
            NetworkLayer.Instance.SendPacket(packet);
            success = _timeout.WaitForTimeout();
            if (!success) return success; 
        }
        return success;
    }

    public void CancelTimeout()
    {
        _timeout.CancelTimeout();
    }
}