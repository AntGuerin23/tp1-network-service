using tp1_network_service.Messages;

namespace tp1_network_service;

internal abstract class Layer
{
    public abstract void StartListening(string filesPath);
    internal abstract void HandleNewMessage(Message message);
}