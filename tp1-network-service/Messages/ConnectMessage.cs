using tp1_network_service.Enums;

namespace tp1_network_service.Messages;

internal class ConnectMessage : Message
{
    public ConnectMessage(Message message) : base(message) { }
}