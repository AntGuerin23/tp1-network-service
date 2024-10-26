using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Handling;

internal interface IPrimitiveHandlerStrategy
{
    public void HandleConnectPrimitive(ConnectPrimitive primitive);
    public void HandleDisconnectPrimitive(DisconnectPrimitive primitive);
    public void HandleDataPrimitive(DataPrimitive primitive);
}