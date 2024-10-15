namespace tp1_network_service.Primitives.Children; 

internal class DisconnectPrimitive : Primitive
{
    public DisconnectReason? Reason { get; }
    
    public DisconnectPrimitive(PrimitiveType type, int connectionNumber, DisconnectReason? reason) : base(type, connectionNumber)
    {
        Reason = reason;
    }

    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }
}