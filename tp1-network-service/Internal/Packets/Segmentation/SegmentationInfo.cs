namespace tp1_network_service.Internal.Packets.Segmentation;

internal class SegmentationInfo
{
    public byte CurrentSegmentNumber { get; set; }
    public bool OtherSegmentsAreToCome { get; set; }
    public byte NextSegmentNumber { get; set; }
    
    private const byte OtherSegsToComeBitwisePosition = 0b00010000;
    private const byte NextSegBitwisePosition = 0b00001110;

    public SegmentationInfo(byte currentSegmentNumber, bool otherSegmentsAreToCome, byte nextSegmentNumber = 0)
    {
        CurrentSegmentNumber = currentSegmentNumber;
        OtherSegmentsAreToCome = otherSegmentsAreToCome;
        NextSegmentNumber = nextSegmentNumber;
    }
    
    public SegmentationInfo(byte segInfoByte)
    {
        CurrentSegmentNumber = (byte) (segInfoByte >> 5); //assigns 3 leftmost bit
        OtherSegmentsAreToCome = ((segInfoByte & OtherSegsToComeBitwisePosition) >> 4) == 1; //assigns 4th bit
        NextSegmentNumber = (byte)(segInfoByte & NextSegBitwisePosition >> 1); //assigns bits 5,6 and 7
        
    }

    public static PacketType GetDataPacketType(byte rawSegInfo)
    {
        return (rawSegInfo & 0b00000001) == 0b1 ? PacketType.DataAcknowledgment : PacketType.Data;
    }
}