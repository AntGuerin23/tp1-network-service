using tp1_network_service.Primitives;

namespace tp1_network_service.Communication;

internal abstract class LayerQueue
{
    protected const string FromTransportToNetworkPath = "FROM_T_TO_R.txt";
    protected const string FromNetworkToTransportPath = "FROM_R_TO_T.txt";

    protected static readonly object FromTransportToNetworkLock = new();
    protected static readonly object FromNetworkToTransportLock = new();

    public static extern Primitive Get();
    public static extern void Put(Primitive primitive);

}