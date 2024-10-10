using tp1_network_service.Enums;

namespace tp1_network_service.Messages;

internal abstract class Primitive
{
    public PrimitiveType Type { get; set; }
    public int ConnectionNumber { get; set; }
    
    protected Primitive(Primitive primitive) //Copy constructor used by builder
    {
        ConnectionNumber = primitive.ConnectionNumber;
        Type = primitive.Type; 
    }

    protected Primitive() { }

    public abstract void Handle(bool isHandledByTransport = false);
    public abstract byte[] Serialize();
}