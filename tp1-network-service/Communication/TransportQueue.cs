using tp1_network_service.Primitives;

namespace tp1_network_service.Communication;

internal class TransportQueue : LayerQueue
{
    public new static Primitive Get()
    {
        byte[] serializedPrimitive;
        lock (FromTransportToNetworkLock)
        {
            serializedPrimitive = FileManager.Read(FromTransportToNetworkPath);
        }

        return Deserializer.DeserializePrimitive(serializedPrimitive);

    }

    public new static void Put(Primitive primitive)
    {
        var serializedPrimitive = primitive.Serialize();
        lock (FromNetworkToTransportLock)
        {
            FileManager.Write(serializedPrimitive, FromNetworkToTransportPath);
        }
    }
}