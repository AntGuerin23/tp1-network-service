namespace tp1_network_service.Messages;

internal class SegmentationInfo
{
    public byte CurrentSegmentNumber { get; set; }
    public bool OtherSegmentsAreToCome { get; set; }
    public byte NextSegmentNumber { get; set; }

    private const byte OtherSegsToComeBitwisePosition = 0b00010000;
    private const byte NextSegBitwisePosition = 0b00001110;
    
    public SegmentationInfo(byte segInfoByte)
    {
        CurrentSegmentNumber = (byte) (segInfoByte >> 5); //assigns 3 leftmost bit
        OtherSegmentsAreToCome = ((segInfoByte & OtherSegsToComeBitwisePosition) >> 4) == 1; //assigns 4th bit
        NextSegmentNumber = (byte)(segInfoByte & NextSegBitwisePosition >> 1); //assigns bits 5,6 and 7 (8 is always 0)
    }
}