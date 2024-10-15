namespace tp1_network_service.Primitives;

internal abstract class Primitive
{
    public PrimitiveType Type { get; set; }
    public int ConnectionNumber { get; set; }
    
    protected Primitive(PrimitiveType type, int connectionNumber)
    {
        Type = type;
        ConnectionNumber = connectionNumber;
    }

    protected Primitive() { }
    public abstract byte[] Serialize();
}