using tp1_network_service.Internal.Builder;
using tp1_network_service.Internal.Enums;
using tp1_network_service.Internal.Layers.Handling;
using tp1_network_service.Internal.Layers.Transport;
using tp1_network_service.Internal.Primitives;
using tp1_network_service.Internal.Primitives.Children;

namespace tp1_network_service.Internal.Layers.Network;

internal class NetworkPrimitiveHandlerStrategy : IPrimitiveHandlerStrategy
{
    public void HandleConnectPrimitive(ConnectPrimitive primitive)
    {
        if (!primitive.IsRequest()) return;
        if (primitive.ConnectionNumber % 27 == 0)
        {
            TransportLayer.Instance.HandleFromLayer(new PrimitiveBuilder()
                .SetConnectionNumber(primitive.ConnectionNumber)
                .SetDestinationAddress(primitive.DestinationAddress)
                .SetSourceAddress(primitive.SourceAddress)
                .SetType(PrimitiveType.Ind)
                .SetReason(DisconnectReason.NetworkService)
                .ToDisconnectPrimitive());
        }
        else
        {
            NetworkLayer.Instance.SendPacket(primitive.GeneratePacket());
            NetworkLayer.Instance.PendingConnectionRequestManager.WaitForResponse(primitive);
        }
    }

    public void HandleDisconnectPrimitive(DisconnectPrimitive primitive)
    {
        if (!primitive.IsRequest()) return;
        NetworkLayer.Instance.SendPacket(primitive.GeneratePacket());
    }

    public void HandleDataPrimitive(DataPrimitive primitive)
    {
        var success = NetworkLayer.Instance.DataSendingManager.StartSendingData(primitive);
        TransportLayer.Instance.HandleFromLayer(new PrimitiveBuilder()
            .SetConnectionNumber(primitive.ConnectionNumber)
            .SetType(PrimitiveType.Ind)
            .SetReason(success ? DisconnectReason.Success : DisconnectReason.Distant)
            .ToDisconnectPrimitive());
    }
}