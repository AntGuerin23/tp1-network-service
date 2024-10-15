using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Interfaces;

internal interface ITransportLayer
{
    public void ConfirmConnection(ConnectPrimitive connectPrimitive);
    public void Disconnect(DisconnectPrimitive disconnectPrimitive);
}