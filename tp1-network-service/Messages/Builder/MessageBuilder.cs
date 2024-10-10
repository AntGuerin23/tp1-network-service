using tp1_network_service.Enums;

namespace tp1_network_service.Messages.Builder;

internal class MessageBuilder
{
    private Message _message;

    public MessageBuilder()
    {
        _message = new BuildableMessage();
    }

    public MessageBuilder Reset()
    {
        _message = new BuildableMessage();
        return this;
    }
    
    public Message GetResult()
    {
        return _message;
    }

    public MessageBuilder SetType(MessageType type)
    {
        _message.Type = type;
        return this;
    }
    
    public MessageBuilder SetConnectionNumber(int connectionNumber)
    {
        _message.ConnectionNumber = connectionNumber;
        return this;
    }

    public MessageBuilder SetSource(byte? source)
    {
        _message.Source = source;
        return this;
    }

    public MessageBuilder SetDestination(byte? destination)
    {
        _message.Destination = destination;
        return this;
    }

    public MessageBuilder SetPrimitive(MessagePrimitive primitive)
    {
        _message.Primitive = primitive;
        return this;
    }
    
    public MessageBuilder ToConnectMessage()
    {
        _message = new ConnectMessage(_message);
        return this;
    }

    public MessageBuilder ToDataMessage(SegmentationInfo segInfo, byte[] data)
    {
        _message = new DataMessage(_message, segInfo, data);
        return this;
    }
    
    public MessageBuilder ToDisconnectMessage(DisconnectReason reason)
    {
        _message = new DisconnectMessage(_message, reason);
        return this;
    }
    
    // Needed because a message is abstract, and the actual type (connect, data, disconnect) is only known at the end of the building process 
    private class BuildableMessage : Message
    {
        public override void Handle(bool isHandledByTransport = false)
        {
            throw new NotImplementedException();
        }
    }
}
