using tp1_network_service.Primitives;

namespace tp1_network_service;

internal class LayerIo
{
    private const string FromTransportToNetworkPath = "FROM_T_TO_R.txt";
    private const string FromNetworkToTransportPath = "FROM_R_TO_T.txt";

    private static readonly object FromTransportToNetworkLock = new();
    private static readonly object FromNetworkToTransportLock = new();

    public static Primitive ReadFromTransport()
    {
        lock (FromTransportToNetworkLock)
        {
            FileManager.Read(FromTransportToNetworkPath);
            //TODO : Deserialize
            throw new NotImplementedException();
        } 
    }

    public static void WriteToTransport(Primitive primitive)
    {
        lock (FromNetworkToTransportLock)
        {
            //TODO: Serialize
            FileManager.Write(Array.Empty<byte>(), FromNetworkToTransportPath);
        }
    }
    
    public static Primitive ReadFromNetwork()
    {
        lock (FromNetworkToTransportLock)
        {
            FileManager.Read(FromNetworkToTransportPath);
            //TODO : Deserialize
            throw new NotImplementedException();
        }
    }

    public static void WriteToNetwork(Primitive primitive)
    {
        lock (FromTransportToNetworkLock)
        {
            //TODO: Serialize
            FileManager.Write(Array.Empty<byte>(), FromTransportToNetworkPath);
        }
    }
}