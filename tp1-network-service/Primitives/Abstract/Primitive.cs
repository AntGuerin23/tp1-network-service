namespace tp1_network_service.Primitives.Abstract;

internal abstract class Primitive : CommunicationEntity
{
    public PrimitiveType Type { get; set; }
    
    protected Primitive(PrimitiveType type, int connectionNumber) : base(connectionNumber)
    {
        Type = type;
    }
}