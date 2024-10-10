using tp1_network_service.Enums;
using tp1_network_service.Layers;

namespace tp1_network_service.Messages;

internal class DisconnectPrimitive(Primitive primitive, DisconnectReason reason) : Primitive(primitive)
{
    public DisconnectReason Reason { get; set; } = reason;
    
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