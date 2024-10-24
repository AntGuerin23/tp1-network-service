using tp1_network_service.Internal.Primitives.Abstract;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Handling;

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