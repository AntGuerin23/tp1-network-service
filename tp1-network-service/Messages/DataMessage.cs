namespace tp1_network_service.Messages;

internal class DataMessage : Message
{
    public SegmentationInfo SegmentationInfo { get; set; }
    public byte[] Data { get; set; }

    public DataMessage(Message message, SegmentationInfo segInfo, byte[] data) : base(message)
    {
        SegmentationInfo = segInfo;
        Data = data;
    }
}