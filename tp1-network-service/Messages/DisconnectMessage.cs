using tp1_network_service.Enums;

namespace tp1_network_service.Messages;

internal class DisconnectMessage : Message
{
    public DisconnectReason Reason { get; set; }

    public DisconnectMessage(Message message, DisconnectReason reason) : base(message)
    {
        Reason = reason;
    }
}