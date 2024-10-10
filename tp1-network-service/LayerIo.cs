using tp1_network_service.Messages;

namespace tp1_network_service.InterLayerCommunication;

internal class LayerIo
{
    private const string FromTransportToNetworkPath = "FROM_T_TO_R.txt";
    private const string FromNetworkToTransportPath = "FROM_R_TO_T.txt";

    protected static object NetworkWriteToLock;
    protected static object NetworkReadFromLock;

    public Primitive ReadFromTransport()
    {
        FileManager.Read(FromTransportToNetworkPath);
        //TODO : Deserialize
        throw new NotImplementedException();
    }

    public void WriteToTransport(Primitive primitive)
    {
        //TODO: Serialize
        FileManager.Write(Array.Empty<byte>(), FromNetworkToTransportPath);
    }
    
    public Primitive ReadFromNetwork()
    {
        FileManager.Read(FromNetworkToTransportPath);
        //TODO : Deserialize
        throw new NotImplementedException();
    }

    public void WriteToNetwork(Primitive primitive)
    {
        //TODO: Serialize
        FileManager.Write(Array.Empty<byte>(), FromTransportToNetworkPath);
    }
}