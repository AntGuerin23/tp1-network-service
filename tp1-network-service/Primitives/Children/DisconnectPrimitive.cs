namespace tp1_network_service.Primitives.Children; 

internal class DisconnectPrimitive(Primitive primitive, DisconnectReason? reason) : Primitive(primitive)
{
    
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