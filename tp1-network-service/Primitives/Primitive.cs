namespace tp1_network_service.Primitives;

internal abstract class Primitive : CommunicationEntity
{
    public PrimitiveType Type { get; set; }
    
    protected Primitive(PrimitiveType type, int connectionNumber)
    {
        Type = type;
        ConnectionNumber = connectionNumber;
    }

    protected Primitive() { }
}