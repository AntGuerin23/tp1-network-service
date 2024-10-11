using tp1_network_service.Primitives;

namespace tp1_network_service.Communication;

internal class NetworkQueue : LayerQueue
{
    public new static Primitive Get()
    {
        byte[] serializedPrimitive;
        lock (FromNetworkToTransportLock)
        {
            serializedPrimitive = FileManager.Read(FromNetworkToTransportPath);
        }
        return Deserializer.DeserializePrimitive(serializedPrimitive);
    }

    public new static void Put(Primitive primitive)
    {
        var serializedPrimitive = primitive.Serialize();
        lock (FromTransportToNetworkLock)
        {
            FileManager.Write(serializedPrimitive, FromTransportToNetworkPath);
        }    }
}