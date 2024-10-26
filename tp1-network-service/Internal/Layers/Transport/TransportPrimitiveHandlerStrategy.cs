using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Layers.Handling;
using tp1_network_service.Internal.Layers.Network;
using tp1_network_service.Internal.Primitives;
using tp1_network_service.Internal.Primitives.Children;
using tp1_network_service.Internal.Utils;

namespace tp1_network_service.Internal.Layers.Transport;

internal class TransportPrimitiveHandlerStrategy : IPrimitiveHandlerStrategy
{
    public void HandleConnectPrimitive(ConnectPrimitive primitive)
    {
        if (!primitive.IsConfirmation()) return;
        var pendingData =
            TransportLayer.Instance.ConnectionsHandler.
                ConfirmWaitingConnectionAndGetPendingData(primitive.ConnectionNumber);
        TransportLayer.Instance.Logger.LogNewConfirmedConnection(primitive);
        var dataPrimitive = new PrimitiveBuilder()
            .SetConnectionNumber(primitive.ConnectionNumber)
            .SetType(PrimitiveType.Ind)
            .SetData(pendingData)
            .ToDataPrimitive();
        TransportLayer.Instance.Logger.LogDataTransmission(dataPrimitive.ConnectionNumber, dataPrimitive.Data);
        NetworkLayer.Instance.HandleFromLayer(dataPrimitive);
    }
    
    public void HandleDisconnectPrimitive(DisconnectPrimitive primitive)
    {
        TransportLayer.Instance.ConnectionsHandler.CloseConnection(primitive.ConnectionNumber);
        TransportLayer.Instance.Logger.LogDisconnectIndication(primitive);
    }

    public void HandleDataPrimitive(DataPrimitive primitive)
    {
        throw new NotImplementedException();
    }
}