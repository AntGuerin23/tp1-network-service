using tp1_network_service.Primitives;
using tp1_network_service.Primitives.Abstract;
using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Layers;

internal class PrimitiveHandler(IPrimitiveHandlerStrategy strategy)
{
    public void Handle(Primitive primitive)
    {
        switch (primitive)
        {
            case ConnectPrimitive connect:
                strategy.HandleConnectPrimitive(connect);
                break;
            case DisconnectPrimitive disconnect:
                strategy.HandleDisconnectPrimitive(disconnect);
                break;
            case DataPrimitive data:
                strategy.HandleDataPrimitive(data);
                break;
        }
    }
}