using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Layers.Handling;
using tp1_network_service.Internal.Layers.Network;
using tp1_network_service.Internal.Primitives;
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
        var dataPrimitive = new PrimitiveBuilder()
            .SetConnectionNumber(primitive.ConnectionNumber)
            .SetType(PrimitiveType.Ind)
            .SetData(pendingData)
            .ToDataPrimitive();
        NetworkLayer.Instance.HandleFromLayer(dataPrimitive);
    }
    
    public void HandleDisconnectPrimitive(DisconnectPrimitive primitive)
    {
        TransportLayer.Instance.ConnectionsHandler.CloseConnection(primitive.ConnectionNumber);
    }

    public void HandleDataPrimitive(DataPrimitive primitive)
    {
        throw new NotImplementedException();
    }
}