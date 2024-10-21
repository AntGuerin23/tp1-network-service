using tp1_network_service.Primitives;

namespace tp1_network_service.Layers.Network;

public class NetworkPrimitiveHandlerStrategy : IPrimitiveHandlerStrategy
{
    public void HandleConnectPrimitive(ConnectPrimitive primitive)
    {
        if (!primitive.IsRequest()) return;
        NetworkLayer.Instance.PendingConnectRequestManager.SetPendingConnect(primitive);
        NetworkLayer.Instance.SendPrimitiveToDataLinkLayer(primitive.ToIndication());
    }

    public void HandleDisconnectPrimitive(DisconnectPrimitive primitive)
    {
        if (!primitive.IsRequest()) return;
        NetworkLayer.Instance.SendPrimitiveToDataLinkLayer(primitive.ToIndication());
    }

    public void HandleDataPrimitive(DataPrimitive primitive)
    {
        if (!primitive.IsRequest()) return;
        NetworkLayer.Instance.SendPrimitiveToDataLinkLayer(primitive.ToIndication());
    }
}