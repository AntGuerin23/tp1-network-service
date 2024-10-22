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
        NetworkLayer.Instance.PendingConnectRequestManager.SetPendingConnect(primitive);
        NetworkLayer.Instance.SendPacket(primitive.GeneratePacket());
    }

    public void HandleDisconnectPrimitive(DisconnectPrimitive primitive)
    {
        if (!primitive.IsRequest()) return;
        NetworkLayer.Instance.SendPacket(primitive.GeneratePacket());
    }

    public void HandleDataPrimitive(DataPrimitive primitive)
    {
        if (!primitive.IsRequest()) return;
        var success = NetworkLayer.Instance.PendingSendingDataManager.StartSendingData(primitive);
        if (!success)
        {
            TransportLayer.Instance.HandleFromLayer(new PrimitiveBuilder()
                .SetConnectionNumber(primitive.ConnectionNumber)
                .SetType(PrimitiveType.Ind)
                .SetReason(DisconnectReason.Distant)
                .ToDisconnectPrimitive());
        }
    }
}