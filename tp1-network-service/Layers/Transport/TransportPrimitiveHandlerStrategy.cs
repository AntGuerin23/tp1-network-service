using tp1_network_service.Primitives;

namespace tp1_network_service.Layers.Transport;

public class TransportPrimitiveHandlerStrategy : IPrimitiveHandlerStrategy
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