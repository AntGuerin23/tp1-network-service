using tp1_network_service.Enum;
using tp1_network_service.Packets;
using tp1_network_service.Packets.Abstract;

namespace tp1_network_service.Builders;

public class PacketBuilder
{
    // Packet fields
    private int _connectionNumber;
    private CommunicationType _type;
    
    // ConnectionPacket fields
    private int _sourceAddress;
    private int _destinationAddress;
    
    // DisconnectPacket fields
    private DisconnectReason _reason;
    
    // DataPacket fields
    private byte[] _data;
    private bool _isSegmented;
    private int _sequenceNumber;
    private int _nextExpectedSequence;

    public PacketBuilder ConnectionNumber(int connectionNumber)
    {
        _connectionNumber = connectionNumber;
        return this;
    }

    public PacketBuilder Type(CommunicationType type)
    {
        _type = type;
        return this;
    }

    public PacketBuilder SourceAddress(int sourceAddress)
    {
        _sourceAddress = sourceAddress;
        return this;
    }

    public PacketBuilder DestinationAddress(int destinationAddress)
    {
        _destinationAddress = destinationAddress;
        return this;
    }

    public PacketBuilder Reason(DisconnectReason reason)
    {
        _reason = reason;
        return this;
    }

    public PacketBuilder Data(byte[] data)
    {
        _data = data;
        return this;
    }

    public PacketBuilder IsSegmented(bool isSegmented)
    {
        _isSegmented = isSegmented;
        return this;
    }

    public PacketBuilder SequenceNumber(int sequenceNumber)
    {
        _sequenceNumber = sequenceNumber;
        return this;
    }

    public PacketBuilder NextExpectedSequence(int nextExpectedSequence)
    {
        _nextExpectedSequence = nextExpectedSequence;
        return this;
    }

    public ConnectPacket ToConnectPacket()
    {
        return new ConnectPacket(_connectionNumber, _type, _sourceAddress, _destinationAddress);
    }

    public DisconnectPacket ToDisconnectPacket()
    {
        return new DisconnectPacket(_connectionNumber, _type, _sourceAddress, _destinationAddress, _reason);
    }

    public DataPacket ToDataPacket()
    {
        return new DataPacket(_connectionNumber, _type, _data, _isSegmented, _sequenceNumber, _nextExpectedSequence);
    }
}