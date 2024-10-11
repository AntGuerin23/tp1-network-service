namespace tp1_network_service.Primitives.Children; 

internal class DisconnectPrimitive : Primitive
{
    public DisconnectReason? Reason { get; }
    
    public DisconnectPrimitive(Primitive primitive, DisconnectReason? reason) : base(primitive)
    {
        Reason = reason;
    }
    
    public override void Handle(bool isHandledByTransport = false)
    {
        // if (isHandledByTransport)
        // {
        //     HandleFromTransport();
        //     return;
        // }
        // HandleFromNetwork();
    }

    public override byte[] Serialize()
    {
        throw new NotImplementedException();
    }
}