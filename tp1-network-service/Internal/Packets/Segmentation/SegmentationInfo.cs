namespace tp1_network_service.Internal.Packets.Segmentation;

internal class SegmentationInfo
{
    public byte CurrentSegmentNumber { get; }
    public bool OtherSegmentsAreToCome { get; }
    public byte NextSegmentNumber { get; }
    
    private const byte OtherSegsToComeBitwisePosition = 0b00010000;
    private const byte CurrentSegBitwisePosition = 0b00001110;

    public SegmentationInfo(byte nextSegmentNumber, bool otherSegmentsAreToCome, byte currentSegmentNumber)
    {
        CurrentSegmentNumber = currentSegmentNumber;
        OtherSegmentsAreToCome = otherSegmentsAreToCome;
        NextSegmentNumber = nextSegmentNumber;
    }
    
    public SegmentationInfo(byte segInfoByte)
    {
        NextSegmentNumber = (byte) (segInfoByte >> 5); //assigns 3 leftmost bit
        OtherSegmentsAreToCome = ((segInfoByte & OtherSegsToComeBitwisePosition) >> 4) == 1; //assigns 4th bit
        CurrentSegmentNumber = (byte)((segInfoByte & CurrentSegBitwisePosition) >> 1); //assigns bits 5,6 and 7
    }

    public byte Serialize()
    {
        byte serialized = 0;

        serialized = (byte) (serialized | (CurrentSegmentNumber << 1));
        serialized = (byte) (serialized | (NextSegmentNumber << 5));

        if (OtherSegmentsAreToCome)
        {
            serialized = (byte) (serialized | OtherSegsToComeBitwisePosition);
        }
        
        return serialized;
    }

    public static byte SerializeAcknowledgment(bool success, byte nextSegmentNumber)
    {
        byte serialized = 0;
        serialized = (byte) (serialized | (nextSegmentNumber << 5));
        serialized = (byte) (serialized | ((success) ? 0b0001 : 0b1001));
        return serialized;
    }

    public static PacketType GetDataPacketType(byte rawSegInfo)
    {
        return (rawSegInfo & 0b00000001) == 0b1 ? PacketType.DataAcknowledgment : PacketType.Data;
    }
}