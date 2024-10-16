namespace tp1_network_service.Packets;

internal class SegmentationInfo
{
    public byte CurrentSegmentNumber { get; set; }
    public bool OtherSegmentsAreToCome { get; set; }
    public byte NextSegmentNumber { get; set; } 
}