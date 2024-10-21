using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Layers;

internal interface IPrimitiveHandlerStrategy
{
    public void HandleConnectPrimitive(ConnectPrimitive primitive);
    public void HandleDisconnectPrimitive(DisconnectPrimitive primitive);
    public void HandleDataPrimitive(DataPrimitive primitive);
}