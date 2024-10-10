using tp1_network_service.Enums;

namespace tp1_network_service.Messages.Builder;

internal class MessageBuilder
{
    private Primitive _primitive;

    public MessageBuilder()
    {
        _primitive = new BuildablePrimitive();
    }

    public MessageBuilder Reset()
    {
        _primitive = new BuildablePrimitive();
        return this;
    }
    
    public Primitive GetResult()
    {
        return _primitive;
    }

    public MessageBuilder SetType(PrimitiveType type)
    {
        _primitive.Type = type;
        return this;
    }
    
    public MessageBuilder SetConnectionNumber(int connectionNumber)
    {
        _primitive.ConnectionNumber = connectionNumber;
        return this;
    }

    public MessageBuilder SetSource(byte? source)
    {
        _primitive.Source = source;
        return this;
    }

    public MessageBuilder SetDestination(byte? destination)
    {
        _primitive.Destination = destination;
        return this;
    }

    public MessageBuilder SetType(MessagePrimitive primitive)
    {
        _primitive.Primitive = primitive;
        return this;
    }
    
    public MessageBuilder ToConnectMessage()
    {
        _primitive = new ConnectPrimitive(_primitive);
        return this;
    }

    public MessageBuilder ToDataMessage(SegmentationInfo segInfo, byte[] data)
    {
        _primitive = new DataPrimitive(_primitive, segInfo, data);
        return this;
    }
    
    public MessageBuilder ToDisconnectMessage(DisconnectReason reason)
    {
        _primitive = new DisconnectPrimitive(_primitive, reason);
        return this;
    }
    
    // Needed because a message is abstract, and the actual type (connect, data, disconnect) is only known at the end of the building process 
    private class BuildablePrimitive : Primitive
    {
        public override void Handle(bool isHandledByTransport = false)
        {
            throw new NotImplementedException();
        }
    }
}
