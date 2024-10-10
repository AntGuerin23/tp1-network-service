using tp1_network_service.Enums;

namespace tp1_network_service.Messages;

internal abstract class Message
{
    public MessagePrimitive Primitive { get; set; }
    public int ConnectionNumber { get; set; }
    public MessageType Type { get; set; }
    public byte? Source { get; set; }
    public byte? Destination { get; set; }
    
    protected Message(Message message) //Copy constructor used by builder
    {
        Primitive = message.Primitive;
        ConnectionNumber = message.ConnectionNumber;
        Type = message.Type;
        Source = message.Source;
        Destination = message.Destination;
    }

    protected Message() { }

    public abstract void Handle(bool isHandledByTransport = false);
}