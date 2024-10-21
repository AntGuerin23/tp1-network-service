using tp1_network_service.Enum;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Packets;

public class DataPacket : Packet
{

    public byte[] Data { get; }
    public bool IsSegmented { get; }
    public int SequenceNumber { get; }
    public int NextExpectedSequence { get; }
    
    public DataPacket(int connectionNumber, CommunicationType type, byte[] data, bool isSegmented, int sequenceNumber, int nextExpectedSequence)
        : base(connectionNumber, type)
    {
        Data = data;
        IsSegmented = isSegmented;
        SequenceNumber = sequenceNumber;
        NextExpectedSequence = nextExpectedSequence;
    }

    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }
}