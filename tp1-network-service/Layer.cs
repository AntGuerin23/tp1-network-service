namespace tp1_network_service;

public abstract class Layer
{
    public abstract void StartListening();
    internal abstract void HandleNewMessage();
}