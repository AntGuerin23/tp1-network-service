namespace tp1_network_service.Internal.Primitives.Abstract;

internal abstract class Primitive : CommunicationEntity
{
    public PrimitiveType Type { get; set; }
    
    public bool IsRequest() => Type == PrimitiveType.Req;
    public bool IsIndication() => Type == PrimitiveType.Ind;
    
    protected Primitive(int connectionNumber, PrimitiveType type) : base(connectionNumber)
    {
        Type = type;
    }
}