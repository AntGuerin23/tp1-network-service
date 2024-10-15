using tp1_network_service.Primitives.Children;

namespace tp1_network_service.Interfaces;

internal interface INetworkLayer
{
    public void Connect(ConnectPrimitive connectPrimitive);
    public void Disconnect(DisconnectPrimitive disconnectPrimitive);
    public void SendData(DataPrimitive dataPrimitive);
}