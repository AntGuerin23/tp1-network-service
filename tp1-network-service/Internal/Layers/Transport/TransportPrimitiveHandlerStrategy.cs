using tp1_network_service.Internal.Layers.Handling;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Transport;

internal class TransportPrimitiveHandlerStrategy : IPrimitiveHandlerStrategy
{
    public void HandleConnectPrimitive(ConnectPrimitive primitive)
    {
        if (!primitive.IsConfirmation()) return;
        var pendingData =
            TransportLayer.Instance.ConnectionsHandler.
                ConfirmWaitingConnectionAndGetPendingData(primitive.ConnectionNumber);
    }
    
    public void HandleDisconnectPrimitive(DisconnectPrimitive primitive)
    {
        throw new NotImplementedException();
    }

    public void HandleDataPrimitive(DataPrimitive primitive)
    {
        throw new NotImplementedException();
    }
}