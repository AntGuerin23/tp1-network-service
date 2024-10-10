using tp1_network_service.Enums;
using tp1_network_service.Layers;

namespace tp1_network_service.Messages;

internal class DisconnectMessage(Message message, DisconnectReason reason) : Message(message)
{
    public DisconnectReason Reason { get; set; } = reason;

    private void HandleFromTransport()
    {
        TransportLayer.Instance.ConnectionsHandler.RemoveConnection(ConnectionNumber);
    }

    private void HandleFromNetwork()
    {
    }

    public override void Handle(bool isHandledByTransport = false)
    {
        if (isHandledByTransport)
        {
            HandleFromTransport();
            return;
        }
        HandleFromNetwork();
    }
}